using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace CognitiveSampleWindows.Model.Services
{
    class SpeechService
    {
        public async void DoSpeech(string text, MediaElement speechElement)
        {
            await _speakTextAsync(text, speechElement);
        }

        async Task _speakTextAsync(string text, MediaElement mediaElement)
        {
            IRandomAccessStream stream = await this._synthesizeTextToSpeechAsync(text);

            await mediaElement.PlayStreamAsync(stream, true);
        }

        async Task<IRandomAccessStream> _synthesizeTextToSpeechAsync(string text)
        {
            // Windows.Storage.Streams.IRandomAccessStream
            IRandomAccessStream stream = null;

            // Windows.Media.SpeechSynthesis.SpeechSynthesizer
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // Windows.Media.SpeechSynthesis.SpeechSynthesisStream
                stream = await synthesizer.SynthesizeTextToStreamAsync(text);
            }

            return (stream);
        }
    }
}
