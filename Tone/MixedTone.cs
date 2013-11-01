using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Tone
{
    public class MixedTone : ITone
    {
        ITone[] tones;

        public MixedTone(ITone[] tones)
        {
            this.tones = tones;
        }

        public IEnumerable<double> GetTones(int sampleRate)
        {
            for (int i = 0; ; i++)
            {
                yield return GetTone((double)i / sampleRate);
            }
        }

        public double GetTone(double sec)
        {
            return tones.Select((p) => p.GetTone(sec)).Sum();
        }
    }
}
