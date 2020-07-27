using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FSKview.Tests
{
    class TestWsprLog
    {
        [Test]
        public void Test_LogFormat_ReadAllLogs()
        {
            string logFolderPath = Path.GetFullPath("../../../../../dev/wspr-logs");
            string[] logFilePaths = Directory.GetFiles(logFolderPath, "*.txt");
            foreach (string logFilePath in logFilePaths)
            {
                Console.WriteLine($"\n{Path.GetFileName(logFilePath)}");
                var log = new WsprLogFile(logFilePath);

                for (int i = 0; i < Math.Min(5, log.spots.Count); i++)
                    Console.WriteLine("  " + log.spots[i]);
            }
        }

        [Test]
        public void Test_LogFormat_73()
        {
            string line = "200715 1006  10 -23 -0.3   7.040078  G6JVT IO90 23           0     1    0";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(7, spot.dt.Month);
            Assert.AreEqual(15, spot.dt.Day);
            Assert.AreEqual(10, spot.dt.Hour);
            Assert.AreEqual(6, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2007151006", spot.timestamp);
            Assert.AreEqual(-23, spot.strength);
            Assert.AreEqual(7.040078, spot.frequencyMHz);
            Assert.AreEqual(7_040_078, spot.frequencyHz);
            Assert.AreEqual("G6JVT", spot.callsign);
            Assert.AreEqual("IO90", spot.grid);
        }

        [Test]
        public void Test_LogFormat_88()
        {
            string line = "200713 1908   4 -17  0.32  14.0970666  DL6NL JO50 27           0     1    0    1  527  0";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(7, spot.dt.Month);
            Assert.AreEqual(13, spot.dt.Day);
            Assert.AreEqual(19, spot.dt.Hour);
            Assert.AreEqual(8, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2007131908", spot.timestamp);
            Assert.AreEqual(-17, spot.strength);
            Assert.AreEqual(14.0970666, spot.frequencyMHz);
            Assert.AreEqual(14_097_066.6, spot.frequencyHz);
            Assert.AreEqual("DL6NL", spot.callsign);
            Assert.AreEqual("JO50", spot.grid);
        }

        [Test]
        public void Test_LogFormat_96()
        {
            string line = "200603 1720 -27 -1.34  10.1401891  N8XEF EM99 37           3  0.13  1  1    0  1  38     1   810";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(6, spot.dt.Month);
            Assert.AreEqual(3, spot.dt.Day);
            Assert.AreEqual(17, spot.dt.Hour);
            Assert.AreEqual(20, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2006031720", spot.timestamp);
            Assert.AreEqual(-27, spot.strength);
            Assert.AreEqual(10.1401891, spot.frequencyMHz);
            Assert.AreEqual(10_140_189.1, spot.frequencyHz);
            Assert.AreEqual("N8XEF", spot.callsign);
            Assert.AreEqual("EM99", spot.grid);
        }
    }
}
