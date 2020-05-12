# ChromaSynth
This library is an extension to Ciastex' Chroma Framework and allows for audio synthesis at runtime.

## Example
The extension is quick and easy to set up. The following is an example of how to generate a 440Hz sine wave and have it play continuously:
```csharp
using Chroma;
using ChromaSynth

public class SomeGame : Game {

    private SineWave sine; // Make sure you have an instance variable in your Game class.

    public SomeGame() {
        sine = new SineWave(Audio, 440); // Initialize the Sine Wave and set it to 440Hz

        // Add a Hook to the Audio System and call "GenerateChunk" in the callback
        Audio.HookPostMixProcessor<float>((chunk, bytes) => {
            sine.GenerateChunk(ref chunk);
        });
    }
}
```

## Features
### Audio Formats
Currently ChromaSynth only supports the Float format.

### Wave Modification
While it is possible to make ChromaSynth take over Audio generation entirely, it is also possible to have the library generate an array of samples ready for further modification.

### Additional Parameters
While, of course, setting the volume for each Wave individually is possible, another feature included in ChromaSynth is Left/Right Stereo Channel Balancing.

### On-the-Fly Modification
All variables such as Frequency, Volume and Left/Right Balance can be updated on-the-fly and changes will instantly be applied, allowing for easy implementation of things such as pitch or volume sliding.

## Supported Waveforms
Currently the following Waveforms are supported:
* Sine
* Triangle
* Square
* Sawtooth