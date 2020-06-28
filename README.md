# FSKview

FSKview is a high-resolution spectrogram for viewing frequency-shift keyed (FSK) signals in real time.

![](dev/screenshot.png)

## Download
FSKview can be downloaded from the [FSKview website](https://swharden.com/software/FSKview)

* **https://swharden.com/software/FSKview**

## Build FSKview from Source
Developers interested in experimenting with this program can run it from its source code using [Visual Studio Community](https://visualstudio.microsoft.com/downloads/) (Free). Open the solution file in the src folder and press F5 to run the program.

## Libraries Used by FSKview
Signal analysis is performed by the [FftSharp](https://github.com/swharden/FftSharp) and [Spectrogram](https://github.com/swharden/Spectrogram) libraries and the sound card interface is provided by [NAudio](https://github.com/naudio/NAudio).

## Supported Platforms
FSKview is written in C# and targets .NET Framework for a Windows-specific build. However, can be compiled to target .NET Core, so a cross-platform version is likely feasible with little additional effort. A cross-platform version of FSKview will be explored following the release of [MAUI](https://devblogs.microsoft.com/dotnet/introducing-net-multi-platform-app-ui/) and .NET 5 which is predicted to be released in the end of 2020.
