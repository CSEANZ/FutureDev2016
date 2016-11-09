using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;

namespace SpeechExample.Utils
{
    public class MicClient
    {
        private MicrophoneRecognitionClient _micClient;
        private string _defaultLocale => "en-US";

        SpeechRecognitionMode _mode = SpeechRecognitionMode.LongDictation;

        private KeyManagement _keyManagement;

        public event EventHandler<PartialSpeechResponseEventArgs> OnPartialResponseReceived;
        public event EventHandler<SpeechResponseEventArgs> OnResponseReceived;

        public MicClient()
        {
            _keyManagement = new KeyManagement();
            _keyManagement.GetSubscriptionKeyFromIsolatedStorage();
        }

        public void Start()
        {
            _micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
               _mode,
               _defaultLocale,
               _keyManagement.SubscriptionKey,
               _keyManagement.SubscriptionKey);

            _micClient.OnMicrophoneStatus += _micClient_OnMicrophoneStatus;
            _micClient.OnPartialResponseReceived += _micClient_OnPartialResponseReceived;
            _micClient.OnResponseReceived += _micClient_OnResponseReceived;
            _micClient.OnConversationError += _micClient_OnConversationError;
            _micClient.StartMicAndRecognition();
        }

        private void _micClient_OnConversationError(object sender, SpeechErrorEventArgs e)
        {
            Debug.WriteLine(e.SpeechErrorText);
        }

        private void _micClient_OnResponseReceived(object sender, SpeechResponseEventArgs e)
        {
            Debug.WriteLine(e.PhraseResponse);
            OnResponseReceived?.Invoke(this, e);
        }

        private void _micClient_OnPartialResponseReceived(object sender, PartialSpeechResponseEventArgs e)
        {
            Debug.WriteLine(e.PartialResult);
            OnPartialResponseReceived?.Invoke(this, e);
        }

        private void _micClient_OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            Debug.WriteLine(e.Recording);
        }
    }
}
