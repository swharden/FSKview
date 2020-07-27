using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public class WsprLogFile
    {
        public readonly string FilePath;
        public readonly List<WsprSpot> spots = new List<WsprSpot>();

        public WsprLogFile(string logFilePath)
        {
            FilePath = Path.GetFullPath(logFilePath);
            if (!File.Exists(FilePath))
                throw new ArgumentException($"file does not exist: {FilePath}");
            Read();
        }

        public void Read()
        {
            spots.Clear();
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    var spot = new WsprSpot(streamReader.ReadLine());
                    if (spot.isValid)
                        spots.Add(spot);
                }
            }
        }
    }
}
