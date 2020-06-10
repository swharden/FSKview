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
        public WsprBand(string name, int dial, int low, int high)
        {
            this.name = name ?? $"?";
            dialFreq = dial;
            lowerFreq = low;
            upperFreq = high;
        }
    }
}
