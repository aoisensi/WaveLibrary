using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Wave
{
    public class SequenceWave : IWave
    {
        IWave[] waves;
        int samplingRate;

        public SequenceWave(IWave[] waves)
        {
            if ((samplingRate = (from p in waves select p.SamplingRate)
                .Aggregate((x, y) => (x == y ? x : 0))) == 0) throw new Exception();
            this.waves = waves;
        }

        public int SamplingRate
        {
            get { return samplingRate; }
        }

        public int Length
        {
            get { return (from s in waves select s.Length).Sum(); }
        }

        public double Read(int n)
        {
            foreach (var wave in waves)
            {
                if (wave.Length > n) return wave.Read(n);
                n -= wave.Length;
            }
            throw new IndexOutOfRangeException();
        }

        public IEnumerable<double> Reads()
        {
            foreach (var wave in waves)
            {
                foreach (var w in wave.Reads())
                {
                    yield return w;
                }
            }
        }
    }
}
