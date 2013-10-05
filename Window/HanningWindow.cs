using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveLibrary.Wave;

namespace WaveLibrary.Window
{
    public class HanningWindow : Window
    {
        public HanningWindow(IWave wave)
        {
            this.wave = wave;
        }

        protected override double Func(double x)
        {
            return 0.5 - (Math.Cos(Math.PI * x * 2.0) * 0.5);
        }
    }
}
