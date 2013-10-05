using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveLibrary.Tone;

namespace WaveLibrary.Wave
{
    public class ToneWave : IWave
    {
        ITone tone;
        int sampleRate;
        double start, length;

        public ToneWave(int sampleRate, ITone tone, double start, double sec)
        {
            this.tone = tone;
            this.sampleRate = sampleRate;
            this.start = start;
            this.length = sec;
        }

        public int SamplingRate
        {
            get { return sampleRate; }
        }

        public int Length
        {
            get { return (int)(sampleRate * length); }
        }

        public double Read(int n)
        {
            return tone.GetTone((double)n / sampleRate);
        }

        public IEnumerable<double> Reads()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return Read(i);
            }
            yield break;
        }
    }
}
