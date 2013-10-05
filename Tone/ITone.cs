using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.Tone
{
    public interface ITone
    {
        IEnumerable<double> GetTones(int sampleRate);

        double GetTone(double sec);
    }
}
