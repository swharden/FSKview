using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public class WsprSpot
    {
        public bool isValid { get; private set; }
        public DateTime dt { get; private set; }
        public string timestamp { get; private set; }
        public double strength { get; private set; }
        //public double deltaT { get; private set; }
        public double frequencyMHz { get; private set; }
        public double frequencyHz { get { return Math.Round(frequencyMHz * 1e6, 1); } }
        public string grid { get; private set; }
        //public double power { get; private set; }
        //public double drift { get; private set; }
        public string callsign { get; private set; }

        public bool isWspr { get; private set; } = true;

        public double ageSec { get { return (DateTime.UtcNow - dt).TotalSeconds; } }
        public int segment { get { return (dt.Minute % 10) / 2; } }

        public WsprSpot(string line)
        {
            // WSPR log files may be in multiple formats as described in:
            // https://github.com/swharden/FSKview/issues/26

            string[] parts = line.Split(' ').Where(x => x.Length > 0).ToArray();

            if (line.Contains(" Rx FT") && parts.Length == 10)
                ParseFT(parts);
            else if (line.Length == 96 && parts.Length == 17)
                Parse96(parts);
            else if (line.Length == 88 && parts.Length == 15)
                Parse88(parts);
            else if (line.Length == 73 && parts.Length == 12)
                Parse73(parts);
            else
                Debug.WriteLine($"unsupported WSPR log line format");


            /*
             * Callsigns enclosed in angle brackets are actually sent as 15-bit hash codes. 
             * If such a code is received by another station before the full callsign has 
             * been received, it will be displayed as <...> on the decoded text line. Once 
             * the full callsign has been received, the decoder will thereafter recognize 
             * the hash code and fill in the blanks. Two very different callsigns might have 
             * the same hash code, but the 15-bit hashcode length ensures that in practice 
             * such collisions will be rare.
             */
            if (!string.IsNullOrWhiteSpace(callsign))
            {
                callsign = callsign.Trim('<').Trim('>');
                if (callsign == "...")
                    callsign = null;
            }

            if (string.IsNullOrWhiteSpace(callsign))
                isValid = false;
        }

        public override string ToString()
        {
            return (isValid) ? $"{callsign} ({strength} dB) from {grid} at {dt}" : "invalid";
        }

        private void ParseFT(string[] parts)
        {
            // WSJT-X 2.2.2
            // 0             1      2  3   4   5   6    7      8     9               
            // 191010_130200|14.080|Rx|FT4|-16|0.2|2012|UA4CCH|YO9NC|-07
            try
            {
                isWspr = false;

                int year = int.Parse(parts[0].Substring(0, 2)) + 2000;
                int month = int.Parse(parts[0].Substring(2, 2));
                int day = int.Parse(parts[0].Substring(4, 2));
                int hour = int.Parse(parts[0].Substring(7, 2));
                int minute = int.Parse(parts[0].Substring(9, 2));
                int second = int.Parse(parts[0].Substring(11, 2));
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = parts[0].Replace("_", "");

                strength = int.Parse(parts[4]);
                frequencyMHz = double.Parse(parts[1], CultureInfo.InvariantCulture);
                frequencyMHz += double.Parse(parts[6], CultureInfo.InvariantCulture) / 1e6;
                callsign = parts[7];
                grid = parts[8];

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        private void Parse96(string[] parts)
        {
            // WSJT-X 2.2.2
            // 0      1    2   3     4          5     6
            // 200603|1720|-27|-1.34|10.1401891|N8XEF|EM99|37|3|0.13|1|1|0|1|38|1|810

            try
            {
                int year = int.Parse(parts[0].Substring(0, 2)) + 2000;
                int month = int.Parse(parts[0].Substring(2, 2));
                int day = int.Parse(parts[0].Substring(4, 2));
                int hour = int.Parse(parts[1].Substring(0, 2));
                int minute = int.Parse(parts[1].Substring(2, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = parts[0]+parts[1];

                strength = int.Parse(parts[2]);
                frequencyMHz = double.Parse(parts[4], CultureInfo.InvariantCulture);
                callsign = parts[5];
                grid = parts[6];

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        private void Parse88(string[] parts)
        {
            // WSJT-X 2.1.2
            // 0      1    2 3   4    5          6     7    
            // 200713|1908|4|-17|0.32|14.0970666|DL6NL|JO50|27|0|1|0|1|527|0

            try
            {
                int year = int.Parse(parts[0].Substring(0, 2)) + 2000;
                int month = int.Parse(parts[0].Substring(2, 2));
                int day = int.Parse(parts[0].Substring(4, 2));
                int hour = int.Parse(parts[1].Substring(0, 2));
                int minute = int.Parse(parts[1].Substring(2, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = parts[0] + parts[1];

                strength = int.Parse(parts[3]);
                frequencyMHz = double.Parse(parts[5], CultureInfo.InvariantCulture);
                callsign = parts[6];
                grid = parts[7];

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        private void Parse73(string[] parts)
        {
            // WSPR classic
            // 0      1    2  3   4    5        6     7
            // 200715|1006|10|-23|-0.3|7.040078|G6JVT|IO90|23|0|1|0

            try
            {
                int year = int.Parse(parts[0].Substring(0, 2)) + 2000;
                int month = int.Parse(parts[0].Substring(2, 2));
                int day = int.Parse(parts[0].Substring(4, 2));
                int hour = int.Parse(parts[1].Substring(0, 2));
                int minute = int.Parse(parts[1].Substring(2, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = parts[0] + parts[1];

                strength = int.Parse(parts[3]);
                frequencyMHz = double.Parse(parts[5], CultureInfo.InvariantCulture);
                callsign = parts[6];
                grid = parts[7];

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }
    }
}
