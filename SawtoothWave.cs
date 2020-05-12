using Chroma.Audio;
using System;

namespace ChromaSynth
{
    public class SawtoothWave : Waveform
    {
        private float LastT = 0.0f;

        public SawtoothWave(AudioManager audioManager, float frequency, float volume = 1.0f) : base(audioManager, SynthWaveform.Sawtooth, frequency, volume) { }

        public override float[] GenerateSamples()
        {
            float p = 1.0f / Frequency;
            for (var i = 0; i < Samples.Length; i += 2)
            {
                var t = TimePerChunk / AudioManager.ChunkSize * (i / 2) * 4;
                Samples[i] = (float)(-(Math.Min(1, 2 * (1 - LeftRightBalance)) * Volume * 2 / Math.PI) * Math.Atan(1.0f / Math.Tan((t + LastT) * Math.PI / p)));
                Samples[i + 1] = (float)(-(Math.Min(1, 2 * LeftRightBalance) * Volume * 2 / Math.PI) * Math.Atan(1.0f / Math.Tan((t + LastT) * Math.PI / p)));

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
