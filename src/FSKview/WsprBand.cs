using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    class WsprBand
    {
        public readonly string name;
        public readonly int dialFreq;
        public readonly int lowerFreq;
        public readonly int upperFreq;
        public WsprBand(string name, int dial)
        {
            this.name = name ?? $"?";
            dialFreq = dial;
            lowerFreq = dial + 1400;
            upperFreq = dial + 1600;
        }
    }
}
