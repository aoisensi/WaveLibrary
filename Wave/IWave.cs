using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Wave
{
    public interface IWave
    {
        int SamplingRate { get; }

        int Length { get; }

        double Read(int n);

        IEnumerable<double> Reads();
    }
}
