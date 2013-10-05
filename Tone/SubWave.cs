using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Tone
{
    public class SubWave : IWave
    {
        IWave original;
        int index, count;

        public SubWave(IWave original, double sec) : this(original, (int)(original.SamplingRate * sec)) { }

        public SubWave(IWave original, double start, double sec) : this(original, (int)(original.SamplingRate * start), (int)(original.SamplingRate * sec)) { }

        public SubWave(IWave original, int length) : this(original, 0, length) { }

        public SubWave(IWave original, int start, int length)
        {
            if (original.Length < (start + length)) throw new IndexOutOfRangeException();
            this.original = original;
            this.index = start;
            this.count = length;
        }

        public int SamplingRate
        {
            get { return original.SamplingRate; }
        }

        public int Length
        {
            get { return count; }
        }

        public bool IsInf
        {
            get { return false; }
        }

        public double Read(int n)
        {
            if (original.IsInf || n < count)
                return original.Read(index + n);
            throw new IndexOutOfRangeException();
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
