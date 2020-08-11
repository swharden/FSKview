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

            WsprBand[] bands =
            {
                new WsprBand("None", 0),
                
                // WSPR bands listed on http://wsprnet.org/
                new WsprBand("WSPR 2200m", 0_136_000),
                new WsprBand("WSPR 630m",  0_474_200),
                new WsprBand("WSPR 160m",  1_836_600),
                new WsprBand("WSPR 80m",   3_568_600),
                new WsprBand("WSPR 60m",   5_287_200),
                new WsprBand("WSPR 40m",   7_038_600),
                new WsprBand("WSPR 30m",  10_138_700),
                new WsprBand("WSPR 20m",  14_095_600),
                new WsprBand("WSPR 17m",  18_104_600),
                new WsprBand("WSPR 15m",  21_094_600),
                new WsprBand("WSPR 12m",  24_924_600),
                new WsprBand("WSPR 10m",  28_124_600),
                new WsprBand("WSPR 6m",   50_293_000),
                new WsprBand("WSPR 2m",  144_488_500),

                /*
                new WsprBand("FT8 80m",  3_573_000),
                new WsprBand("FT8 40m",  7_074_000),
                new WsprBand("FT8 30m", 10_136_000),
                new WsprBand("FT8 20m", 14_074_000),
                new WsprBand("FT8 17m", 18_100_000),
                new WsprBand("FT8 15m", 21_074_000),
                new WsprBand("FT8 12m", 24_915_000),
                new WsprBand("FT8 10m", 28_074_000),
                new WsprBand("FT8 6m",  50_310_000),

                new WsprBand("FT4 80m",  3_575_000),
                new WsprBand("FT4 40m",  7_047_500),
                new WsprBand("FT4 30m", 10_140_000),
                new WsprBand("FT4 20m", 14_080_000),
                new WsprBand("FT4 17m", 18_104_000),
                new WsprBand("FT4 15m", 21_140_000),
                new WsprBand("FT4 12m", 24_919_000),
                new WsprBand("FT4 10m", 28_180_000),
                new WsprBand("FT4 6m",  50_318_000),

                new WsprBand("JS8 40m",  7_078_000),
                */
            };

            if (ascending == false)
                Array.Reverse(bands);

            return bands;
        }
    }
}
