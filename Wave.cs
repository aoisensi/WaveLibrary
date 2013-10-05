using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveLibrary.IO;

namespace WaveLibrary
{
    public class Wave : IWave
    {
        double[] samples;

        int sampleRate;

        public static Wave FromStream(Stream stream)
        {
            return WaveStream.Read(stream);
        }

        public static Wave FromPath(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            return FromStream(fs);
        }

        public Wave(double[] samples, int sampleRate)
        {
            this.samples = samples;
            this.sampleRate = sampleRate;
        }

        public IEnumerable<double> Reads()
        {
            return samples;
        }

        public double Read(int n)
        {
            return samples[n];
        }

        public int Length
        {
            get { return samples.Length; }
        }

        public int SamplingRate
        {
            get { return sampleRate; }
        }

        public bool IsInf
        {
            get { return false; }
        }
    }
}
