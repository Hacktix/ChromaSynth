using Chroma.Audio;
using System;

namespace ChromaSynth
{
    public class SquareWave : Waveform
    {
        private float LastT = 0.0f;

        public SquareWave(AudioManager audioManager, float frequency, float volume = 1.0f) : base(audioManager, SynthWaveform.Square, frequency, volume) { }

        public override float[] GenerateSamples()
        {
            int flip = (int)(ChunksPerSecond * AudioManager.ChunkSize / Frequency / 2);
            for (var i = 0; i < Samples.Length; i += 2)
            {
                if (Frequency == 0)
                {
                    Samples[i] = 0;
                    Samples[i + 1] = 0;
                    continue;
                }
                if ((i / 2 + LastT) % flip < flip / 2)
                {
                    Samples[i] = Math.Min(1, 2 * (1 - LeftRightBalance)) * Volume;
                    Samples[i + 1] = Math.Min(1, 2 * LeftRightBalance) * Volume;
                }
                else
                {
                    Samples[i] = Math.Min(1, 2 * (1 - LeftRightBalance)) * -Volume;
                    Samples[i + 1] = Math.Min(1, 2 * LeftRightBalance) * -Volume;
                }

                if (i + 2 >= Samples.Length)
                {
                    LastT += i / 2;
                    while (LastT > flip)
                        LastT -= flip;
                }
            }

            return Samples;
        }
    }
}
