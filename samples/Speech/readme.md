# Speech Recognition Example

This example uses [Cognitive Services](http://aka.ms/cognitiveservices) to listen to the speaker's voice and display the result as a Star Wars style intro scroller. 

It is specifically using the [Bing Speech API](https://www.microsoft.com/cognitive-services/en-us/speech-api). You can sign up for a free trial on the site. 

The example is based on the SDK example [here](https://github.com/Microsoft/Cognitive-Speech-STT-Windows).

It uses Nuget pacakges to bring in the Speak Recognition functionality Microsoft.ProjectOxford.SpeechRecognition-x64 and Microsoft.ProjectOxford.SpeechRecognition-x86.

Special thanks to thanks to @Andrey [http://stackoverflow.com/questions/3157341/starwars-text-effect-in-wpf](http://stackoverflow.com/questions/3157341/starwars-text-effect-in-wpf) for the Star Wars effect XAML!

The following code from MicClient.cs is what starts the process, follow along from there!

```C#
_micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
_mode,
_defaultLocale,
_keyManagement.SubscriptionKey,
_keyManagement.SubscriptionKey);

```
