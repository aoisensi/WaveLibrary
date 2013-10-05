using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Tone
{
    public struct Pitch
    {
        public double Hz;

        public static Pitch FromScale(int scale)
        {
            double hz = (Math.Pow(2.0, ((scale - 69) / 12.0))) * 440.0;
            return new Pitch { Hz = hz };
        }

        public int ToScale()
        {
            return 69 + (int)(12 * Math.Log(Hz / 440.0, 2.0));
        }
    }
}
