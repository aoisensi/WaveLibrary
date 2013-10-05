using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Window
{
    public abstract class Window : IWave
    {
        protected IWave wave;

        public int SamplingRate
        {
            get { return wave.SamplingRate; }
        }

        public int Length
        {
            get { return wave.Length; }
        }

        public double Read(int n)
        {
            return Func(((double)n) / Length) * wave.Read(n);
        }

        public IEnumerable<double> Reads()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return Read(i);
            }
            yield break;
        }

        protected virtual double Func(double x)
        {
            return 1.0;
        }
    }
}
