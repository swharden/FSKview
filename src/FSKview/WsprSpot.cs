using System;
using System.Collections.Generic;
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

        public double ageSec { get { return (DateTime.UtcNow - dt).TotalSeconds; } }
        public int segment { get { return (dt.Minute % 10) / 2; } }

        public WsprSpot(string line)
        {
            // WSPR log files may be in multiple formats as described in:
            // https://github.com/swharden/FSKview/issues/26

            if (line.Length == 96)
                Parse96(line);
            else if (line.Length == 88)
                Parse88(line);
            else if (line.Length == 73)
                Parse73(line);
            else
                throw new InvalidOperationException($"unsupported WSPR log line format");

            if (!string.IsNullOrWhiteSpace(callsign))
                isValid = true;

            /*
             * Callsigns enclosed in angle brackets are actually sent as 15-bit hash codes. 
             * If such a code is received by another station before the full callsign has 
             * been received, it will be displayed as <...> on the decoded text line. Once 
             * the full callsign has been received, the decoder will thereafter recognize 
             * the hash code and fill in the blanks. Two very different callsigns might have 
             * the same hash code, but the 15-bit hashcode length ensures that in practice 
             * such collisions will be rare.
             */
            if (callsign == "...")
                isValid = false;
        }

        public override string ToString()
        {
            return (isValid) ? $"{callsign} ({strength} dB) from {grid} at {dt}" : "invalid";
        }

        private void Parse96(string line)
        {
            // WSJT-X 2.2.2 Format (line length 96)
            // 0      1    2   3      4           5     6    7            8  9     10 11   12 13 14     15  16
            // 200603 1720 -27 -1.34  10.1401891  N8XEF EM99 37           3  0.13  1  1    0  1  38     1   810
            // 000000000011111111112222222222333333333344444444445555555555666666666677777777778888888888999999

            try
            {
                int year = int.Parse(line.Substring(0, 2)) + 2000;
                int month = int.Parse(line.Substring(2, 2));
                int day = int.Parse(line.Substring(4, 2));
                int hour = int.Parse(line.Substring(7, 2));
                int minute = int.Parse(line.Substring(9, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = line.Substring(0, 6) + line.Substring(7, 4);

                strength = int.Parse(line.Substring(11, 4));
                frequencyMHz = double.Parse(line.Substring(21, 12));
                callsign = line.Substring(33, 7).Trim();
                grid = line.Substring(40, 5).Trim();

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        private void Parse88(string line)
        {
            // WSJT-X 2.1.2 Format (line length 88)
            // 0      1      2 3    4     5           6     7    8            9    10   11   12  13  14
            // 200713 1908   4 -17  0.32  14.0970666  DL6NL JO50 27           0     1    0    1  527  0
            // 0000000000111111111122222222223333333333444444444455555555556666666666777777777788888888

            try
            {
                int year = int.Parse(line.Substring(0, 2)) + 2000;
                int month = int.Parse(line.Substring(2, 2));
                int day = int.Parse(line.Substring(4, 2));
                int hour = int.Parse(line.Substring(7, 2));
                int minute = int.Parse(line.Substring(9, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = line.Substring(0, 6) + line.Substring(7, 4);

                strength = int.Parse(line.Substring(15, 4));
                frequencyMHz = double.Parse(line.Substring(25, 12));
                callsign = line.Substring(37, 7).Trim().Trim('<').Trim('>');
                grid = line.Substring(44, 5).Trim();

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        private void Parse73(string line)
        {
            // WSPR (line length 73)
            // 200715 1006  10 -23 -0.3   7.040078  G6JVT IO90 23           0     1    0
            // 0000000000111111111122222222223333333333444444444455555555556666666666777

            try
            {
                int year = int.Parse(line.Substring(0, 2)) + 2000;
                int month = int.Parse(line.Substring(2, 2));
                int day = int.Parse(line.Substring(4, 2));
                int hour = int.Parse(line.Substring(7, 2));
                int minute = int.Parse(line.Substring(9, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = line.Substring(0, 6) + line.Substring(7, 4);

                strength = int.Parse(line.Substring(15, 4));
                frequencyMHz = double.Parse(line.Substring(24, 11));
                callsign = line.Substring(35, 7).Trim().Trim('<').Trim('>');
                grid = line.Substring(42, 5).Trim();

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }
    }
}
