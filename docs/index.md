**FSKview is a high-resolution spectrogram for viewing narrowband frequency-shift keyed (FSK) signals in real time.** FSKview can be used as a [QRSS](#QRSS) viewer, and if [WSTJ-X](https://physics.princeton.edu/pulsar/K1JT/wsjtx.html) is running FSKview can label [WSPR](WSPR.md) spots directly on the spectrogram as they are decoded.

![](screenshot.png)

## Download

> **⚠️ WARNING:** FSKview is early in development and a click-to-run program is not yet provided.

## Configure FSKview as a QRSS Grabber

> **⚠️ WARNING:** An automated FTP upload mechanism has not yet been created.

FSKview can serve as a QRSS grabber, saving the latest grab every 10 minutes. Output images are stamped with the time and date as well as station information.

![](grabber.png)

This video shows my system listening to the 30m WSPR band over a 24 hour period. I don't have a great antenna, but it gets the job done well enough to demonstrate how this software works. The task manager is left open so you can see FSKview is not particularly resource intensive.

<iframe src="https://www.youtube.com/embed/gvbpA6TefgQ" 
    frameborder="0" 
    width="640"
    height="480"
    allowfullscreen
    style="top: 0; left: 0; width: 100%;"
>
</iframe>

## Developer Notes

### Build FSKview from Source
Developers interested in experimenting with this program can run it from its source code using [Visual Studio Community](https://visualstudio.microsoft.com/downloads/) (Free). Open the solution file in the src folder and press F5 to run the program.

### Libraries Used by FSKview

Signal analysis is performed by the [FftSharp](https://github.com/swharden/FftSharp) and [Spectrogram](https://github.com/swharden/Spectrogram) libraries and the sound card interface is provided by [NAudio](https://github.com/naudio/NAudio).

### Anatomy of a WSPR Transmission

#### My WSPR Protocol Notes

* [Encoding and Decoding WSPR Messages](WSPR.md)

#### Additional WSPR Resources

* [WSPR](https://en.wikipedia.org/wiki/WSPR_(amateur_radio_software)) on Wikipedia
* [K1JT Program](http://physics.princeton.edu/pulsar/K1JT/devel.html)
* [WSJT](https://sourceforge.net/projects/wsjt/) on SourceForge
* [WSPR Mode in WSJT-X](https://wsprnet.org/drupal/node/5563)

## Resources

### What is QRSS?

QRSS is a type of continuous wave (CW) radio signal which uses frequency-shift keying (FSK). QRSS is ultra-narrowband (5Hz bandwidth) and ultra slow speed (about 3 letters per minute). The abbreviation QRSS is [Morse code slang](https://en.wikipedia.org/wiki/Q_code) for "transmit a lot slower".

### Notes
* [Encoding and Decoding WSPR Messages](WSPR.md)

### Introduction to QRSS
  * [What is QRSS?](https://www.qsl.net/m0ayf/What-is-QRSS.html)
  * [QRSS and you](http://www.ka7oei.com/qrss1.html)
  * [QRSS (slow CW)](https://sites.google.com/site/qrssinfo/QRSS-Slow-CW)

### Technical Pages
  * [Simulation of QRSS Signals](https://www.qsl.net/pa2ohh/12qrsssim1.htm)

### Other QRSS Software

* Argo ([website](http://digilander.libero.it/i2phd/argo/)) - closed-source QRSS viewer for Windows
* SpectrumLab ([website](http://www.qsl.net/dl4yhf/spectra1.html)) - closed-source spectrum analyzer for Windows 
* QrssPIG ([gitlab](https://gitlab.com/hb9fxx/qrsspig)) - open-source spectrograph for Raspberry Pi (C++)
* Lopora ([website](http://www.qsl.net/pa2ohh/11lop.htm)) - open-source spectrograph (Python 3) 
* QRSS VD ([github](https://github.com/swharden/QRSS-VD)) - open source spectrograph (Python 2)

### QRSS Hardware
This repository is exclusively devoted to software. I run a separate repository dedicated to theory, design, construction, and testing of QRSS transmission and reception hardware: https://github.com/swharden/QRSS-hardware
