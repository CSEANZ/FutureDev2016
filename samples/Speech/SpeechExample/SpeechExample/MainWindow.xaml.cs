using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SpeechExample.Utils;


//thanks to @Andrey http://stackoverflow.com/questions/3157341/starwars-text-effect-in-wpf for the SW text effect!

namespace SpeechExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyManagement _keyManagement = new KeyManagement();
        MicClient _micClient = new MicClient();

        private string _testText =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. <LineBreak/> <LineBreak/> Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.<LineBreak/> <LineBreak/>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.<LineBreak/> <LineBreak/>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.<LineBreak/> <LineBreak/>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.<LineBreak/> <LineBreak/>Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private int _count = 0;

        private string _realText = "";

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += MainWindow_KeyDown;



            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _keyManagement.GetSubscriptionKeyFromIsolatedStorage();

            if (string.IsNullOrWhiteSpace(_keyManagement.SubscriptionKey))
            {
                _showKeyEntry();
            }

            _micClient.OnPartialResponseReceived += _micClient_OnPartialResponseReceived;

            _testSystem();
        }

        void _testSystem()
        {
            var t = new Timer();
            t.Interval = 500;
            t.Elapsed += T_Elapsed;
            t.Start();

        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(_increment));
        }

        void _increment()
        {
            SWText.Text += _testText.Substring(_count, 1);
            _count++;

        }

        void _showKeyEntry()
        {
            var w = new KeyWindow();

            w.ShowDialog();
        }


        void _start()
        {
            _micClient.Start();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.K)
            {
                _showKeyEntry();
            }

            if (e.Key == Key.Space)
            {
                _start();
            }
        }


        private void _micClient_OnPartialResponseReceived(object sender, Microsoft.CognitiveServices.SpeechRecognition.PartialSpeechResponseEventArgs e)
        {
            _realText += e.PartialResult;
        }
    }
}
