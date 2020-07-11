using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    class WsprBands
    {
        public static WsprBand[] GetBands(bool ascending = true)
        {
            // list came from the top left corner of http://wsprnet.org/
            // 0.136, 0.4742, 1.8366, 3.5686, 5.2872, 5364.7, 7.0386, 
            // 10.1387, 14.0956, 18.1046, 21.0946, 24.9246, 28.1246, 
            // 50.293, 70.091, 144.489, 432.300, 1296.500

            WsprBand[] bands =
            {
                new WsprBand("None", 0),
                new WsprBand("160m", 1836600),
                new WsprBand("80m", 3568600),
                new WsprBand("60m", 5287200),
                new WsprBand("40m", 7038600),
                new WsprBand("30m", 10138700),
                new WsprBand("20m", 14095600),
                new WsprBand("17m", 18104600),
                new WsprBand("15m", 21094600),
                new WsprBand("12m", 24924600),
                new WsprBand("10m", 28124600),
                new WsprBand("6m", 50293000),
                new WsprBand("2m", 144488500)
            };

            if (ascending == false)
                Array.Reverse(bands);

            return bands;
        }
    }
}
