# Cognitive Services Vision Sample

This example demonstrates how to send images in to the Cognitive Services system and get back results. 

## Get a Key

You will need to sign up for a Cognitive Services key using the instructions [here](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/signup.md). 

You will need to set up your app secrets using the instructions [here](https://github.com/MSFTAuDX/FutureDev2016/blob/master/docs/appsecrets.md).

Once you have the key you will need to set the secret value "CognitiveServicesKey" for the console app. 

In the UWP, press "K" when the app boots and enter the key. 

## .NET Core Console App

The console app takes an image file as an argument and passes back the identified result. 

It does not use a pre-built SDK, opting to post an image to the end point. 

This example also demonstrates how to use .NET Core configuration in a console app. 

```c#
var result = url.PostAndParse<VisionResponse>(image).GetAwaiter().GetResult();

```

"PostAndParse" is an extension method provided by [ExtensionGoo](https://github.com/jakkaj/ExtensionGoo) - which makes it super easy to do posts and things using string extensions. 

## Windows UWP App
This version of the demo uses the same simple method to send a byte[] image to the server. 

Most of the app code is to setup the camera preview. The basis of this code is from the [Windows Universal Samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraGetPreviewFrame) repo. 

This app also speaks the result - it does not use Cognitive Services to do this (although it could) opting instead to use UWP API to do this. 

```c#
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
```

It then takes this stream and plays it in a normal MediaElement. 