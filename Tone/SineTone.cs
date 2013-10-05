using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Tone
{
    public class SineTone : ITone
    {
        Pitch pitch;
        double volume;

        public SineTone(Pitch pitch, double volume)
        {
            this.pitch = pitch;
            this.volume = volume;
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
            return Math.Sin(sec * pitch.Hz * Math.PI * 2) * volume;
        }
    }
}
