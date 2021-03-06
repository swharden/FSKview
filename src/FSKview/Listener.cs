﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSKview
{
    /* The Listener handles incoming sound card buffers so you don't have to.
     * 
     * The Listener class uses NAudio to connect to an audio device, continuously
     * handle incoming audio buffers, and add incoming audio values to a List<double>.
     * The GetAudio() method returns (and removes) values from this incoming audio list.
     * 
     */
    public class Listener : IDisposable
    {
        public readonly int SampleRate;
        private readonly NAudio.Wave.WaveInEvent wvin;
        public double AmplitudeFrac { get; private set; }
        public int TotalSamples { get; private set; }
        public double TotalTimeSec { get { return (double)TotalSamples / SampleRate; } }
        private readonly List<double> audio = new List<double>();
        public int SamplesInMemory { get { return audio.Count; } }
        readonly Thread t;

        public Listener(int deviceIndex, int sampleRate)
        {
            SampleRate = sampleRate;
            wvin = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = deviceIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bits: 16, channels: 1),
                BufferMilliseconds = 20
            };
            wvin.DataAvailable += OnNewAudioData;
            wvin.StartRecording();

            t = new Thread(AddBufferToAudioForever);
            t.Start();
        }

        public void Dispose()
        {
            shuttingDown = true;
            wvin?.StopRecording();
            wvin?.Dispose();
        }

        private double[] buffer;
        private void OnNewAudioData(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int newSampleCount = args.BytesRecorded / bytesPerSample;
            if (buffer is null)
                buffer = new double[newSampleCount];

            double peak = 0;
            for (int i = 0; i < newSampleCount; i++)
            {
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
                peak = Math.Max(peak, buffer[i]);
            }
            AmplitudeFrac = peak / (1 << 15);
            TotalSamples += newSampleCount;
        }

        private bool shuttingDown = false;
        public void AddBufferToAudioForever()
        {
            int lastSampleAnalyzed = 0;
            while (shuttingDown == false)
            {
                if (lastSampleAnalyzed != TotalSamples)
                {
                    lock (audio)
                    {
                        audio.AddRange(buffer);
                    }
                    lastSampleAnalyzed = TotalSamples;
                }
                Thread.Sleep(1);
            }
        }

        public double[] GetNewAudio()
        {
            lock (audio)
            {
                double[] values = new double[audio.Count];
                for (int i = 0; i < values.Length; i++)
                    values[i] = audio[i];
                audio.RemoveRange(0, values.Length);
                return values;
            }
        }
    }
}
