using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary
{
    public interface IWave
    {
        int SamplingRate { get; }

        int Length { get; }

        bool IsInf { get; }

        double Read(int n);

        IEnumerable<double> Reads();
    }
}
