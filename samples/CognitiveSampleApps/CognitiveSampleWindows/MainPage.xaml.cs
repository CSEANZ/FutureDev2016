using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.System;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CognitiveSampleWindows.Model.Services;
using Panel = Windows.Devices.Enumeration.Panel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CognitiveSampleWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SettingsService _settings = new SettingsService();
        MediaCapture _mediaCapture;
        DisplayRequest _displayRequest;

        private bool _isInitialized;
        private bool _isPreviewing;

        // Information about the camera device
        private bool _mirroringPreview;
        private bool _externalCamera;

        private SimpleOrientation _deviceOrientation = SimpleOrientation.NotRotated;
        private DisplayOrientations _displayOrientation = DisplayOrientations.Portrait;

        private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();
        private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();

        private StorageFolder _captureFolder = null;

        private string _keySet;

        SpeechService _speechService = new SpeechService();

        Windows.Devices.Enumeration.Panel _currentPanel = Panel.Back;

        public MainPage()
        {
            this.InitializeComponent();

            this.KeyDown += MainPage_KeyDown;
        }

        public string KeySet
        {
            get { return _keySet; }
            set
            {
                _keySet = value;
                _settings.SetKey(_keySet);
            }
        }

        private async void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space)
            {
                _doCap();
            }

            if (e.Key == VirtualKey.K)
            {
                _showDialog();
            }

            if (e.Key == VirtualKey.X)
            {
                await CleanupCameraAsync();
                await SetupCamera();
            }
        }

        async void _showDialog()
        {
            await EnterKeyDialog.ShowAsync();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _keySet = _settings.GetKey();

            if (_keySet == null)
            {
                _showDialog();
            }

            await SetupCamera();
            await SetupUiAsync();
        }

        private async Task SetupUiAsync()
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;

            // Populate orientation variables with the current state
            _displayOrientation = _displayInformation.CurrentOrientation;
            if (_orientationSensor != null)
            {
                _deviceOrientation = _orientationSensor.GetCurrentOrientation();
            }

            var picturesLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            // Fall back to the local app storage if the Pictures Library is not available
            _captureFolder = picturesLibrary.SaveFolder ?? ApplicationData.Current.LocalFolder;
        }

        async Task SetupCamera()
        {
            var cameraDevice = await FindCameraDeviceByPanelAsync(_currentPanel);

            if (_currentPanel == Panel.Back)
            {
                _currentPanel = Panel.Front;
            }
            else
            {
                _currentPanel = Panel.Back;
            }
            if (cameraDevice == null)
            {
                Debug.WriteLine("No camera device found!");
                return;
            }

            var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };

            _mediaCapture = new MediaCapture();

            try
            {
                await _mediaCapture.InitializeAsync(settings);
                _isInitialized = true;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("The app was denied access to the camera");
            }

            if (_isInitialized)
            {
                // Figure out where the camera is located
                if (cameraDevice.EnclosureLocation == null || cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown)
                {
                    // No information on the location of the camera, assume it's an external camera, not integrated on the device
                    _externalCamera = true;
                }
                else
                {
                    // Camera is fixed on the device
                    _externalCamera = false;

                    // Only mirror the preview if the camera is on the front panel
                    _mirroringPreview = (cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
                }

                await StartPreviewAsync();


            }
        }

        public async Task<byte[]> Capture()
        {
            var stream = new InMemoryRandomAccessStream();

            Debug.WriteLine("Taking photo...");
            await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);

            try
            {
                var file = await _captureFolder.CreateFileAsync("SimplePhoto.jpg", CreationCollisionOption.GenerateUniqueName);

                Debug.WriteLine("Photo taken! Saving to " + file.Path);

                var photoOrientation = ConvertOrientationToPhotoOrientation(GetCameraOrientation());

                var bytes = await ReencodeAndSavePhotoAsync(stream, file, photoOrientation);
                return bytes;
            }
            catch (Exception ex)
            {
                // File I/O errors are reported as exceptions
                Debug.WriteLine("Exception when taking a photo: " + ex.ToString());
            }

            return null;
        }

        private static PhotoOrientation ConvertOrientationToPhotoOrientation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return PhotoOrientation.Rotate90;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return PhotoOrientation.Rotate180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return PhotoOrientation.Rotate270;
                case SimpleOrientation.NotRotated:
                default:
                    return PhotoOrientation.Normal;
            }
        }

        private SimpleOrientation GetCameraOrientation()
        {
            if (_externalCamera)
            {
                // Cameras that are not attached to the device do not rotate along with it, so apply no rotation
                return SimpleOrientation.NotRotated;
            }

            var result = _deviceOrientation;

            // Account for the fact that, on portrait-first devices, the camera sensor is mounted at a 90 degree offset to the native orientation
            if (_displayInformation.NativeOrientation == DisplayOrientations.Portrait)
            {
                switch (result)
                {
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:
                        result = SimpleOrientation.NotRotated;
                        break;
                    case SimpleOrientation.Rotated180DegreesCounterclockwise:
                        result = SimpleOrientation.Rotated90DegreesCounterclockwise;
                        break;
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:
                        result = SimpleOrientation.Rotated180DegreesCounterclockwise;
                        break;
                    case SimpleOrientation.NotRotated:
                        result = SimpleOrientation.Rotated270DegreesCounterclockwise;
                        break;
                }
            }

            // If the preview is being mirrored for a front-facing camera, then the rotation should be inverted
            if (_mirroringPreview)
            {
                // This only affects the 90 and 270 degree cases, because rotating 0 and 180 degrees is the same clockwise and counter-clockwise
                switch (result)
                {
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:
                        return SimpleOrientation.Rotated270DegreesCounterclockwise;
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:
                        return SimpleOrientation.Rotated90DegreesCounterclockwise;
                }
            }

            return result;
        }

        private async Task StartPreviewAsync()
        {
            try
            {
                PreviewControl.Source = _mediaCapture;
                PreviewControl.FlowDirection = _mirroringPreview ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

                await _mediaCapture.StartPreviewAsync();

                _isPreviewing = true;

               
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            }
            catch (UnauthorizedAccessException)
            {
                // This will be thrown if the user denied access to the camera in privacy settings
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed. {0}", ex.Message);
            }
        }

        private async Task CleanupCameraAsync()
        {
            if (_mediaCapture != null)
            {
                if (_isPreviewing)
                {
                    await _mediaCapture.StopPreviewAsync();
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PreviewControl.Source = null;
                    if (_displayRequest != null)
                    {
                        _displayRequest.RequestRelease();
                    }

                    _mediaCapture.Dispose();
                    _mediaCapture = null;
                });
            }

        }

        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            // Get available devices for capturing pictures
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // Get the desired camera by panel
            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            // If there is no device mounted on the desired panel, return the first device found
            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }


        /// <summary>
        /// Applies the given orientation to a photo stream and saves it as a StorageFile
        /// </summary>
        /// <param name="stream">The photo stream</param>
        /// <param name="file">The StorageFile in which the photo stream will be saved</param>
        /// <param name="photoOrientation">The orientation metadata to apply to the photo</param>
        /// <returns></returns>
        private static async Task<byte[]> ReencodeAndSavePhotoAsync(IRandomAccessStream stream, StorageFile file,
            PhotoOrientation photoOrientation)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                    var properties = new BitmapPropertySet
                    {
                        {"System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16)}
                    };

                    await encoder.BitmapProperties.SetPropertiesAsync(properties);
                    await encoder.FlushAsync();

                    var read = outputStream.AsStreamForRead();
                    read.Seek(0, SeekOrigin.Begin);

                    using (var memoryStream = new MemoryStream())
                    {

                        read.CopyTo(memoryStream);
                        var result = memoryStream.ToArray();
                        return result;
                    }
                }
            }
        }

        private void Capture_OnClick(object sender, RoutedEventArgs e)
        {
            _doCap();
        }

        async void _doCap()
        {
            ProgressRing.IsActive = true;
            ResultText.Text = "";

            _speechService.DoSpeech("okay let me see...", this.SpeechElement);

            var bytes = await Capture();

            if (bytes == null)
            {
                return;
            }

            var service = new VisionService(_settings);

            var result = await service.DetectImage(bytes);

            ProgressRing.IsActive = false;

            var description = result?.description?.captions?.FirstOrDefault()?.text;

            if (description != null)
            {
                description = "it's " + description;
                _speechService.DoSpeech(description, this.SpeechElement);
                ResultText.Text = description;
            }
        }
    }
}
