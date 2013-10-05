using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveLibrary.IO;

namespace WaveLibrary.Wave
{
    public class BufferWave : IWave
    {
        double[] samples;

        int sampleRate;

        public static BufferWave FromStream(Stream stream)
        {
            return WaveStream.Read(stream);
        }

        public static BufferWave FromPath(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            return FromStream(fs);
        }

        public BufferWave(double[] samples, int sampleRate)
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
    }
}
