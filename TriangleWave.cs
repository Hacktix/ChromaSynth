using Chroma.Audio;
using System;

namespace ChromaSynth
{
    public class TriangleWave : Waveform
    {
        private float LastT = 0.0f;

        public TriangleWave(AudioManager audioManager, float frequency, float volume = 1.0f) : base(audioManager, SynthWaveform.Triangle, frequency, volume) { }

        public override float[] GenerateSamples()
        {
            for (var i = 0; i < Samples.Length; i += 2)
            {
                var t = TimePerChunk / AudioManager.ChunkSize * (i / 2) * 4;
                Samples[i] = Math.Min(1, 2 * (1 - LeftRightBalance)) * Volume * (float)(2 / Math.PI * Math.Asin(Math.Sin(2 * Math.PI / (1.0f / Frequency) * (t + LastT))));
                Samples[i + 1] = Math.Min(1, 2 * LeftRightBalance) * Volume * (float)(2 / Math.PI * Math.Asin(Math.Sin(2 * Math.PI / (1.0f / Frequency) * (t + LastT))));

                if (i + 2 >= Samples.Length)
                {
                    LastT = t + LastT;
                    if (LastT / (2 * Math.PI) > 0)
                        LastT -= (float)((int)(LastT / (2 * Math.PI)) * 2 * Math.PI);
                }
            }

            return Samples;
        }
    }
}
