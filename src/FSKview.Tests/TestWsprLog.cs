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
                Assert.Greater(log.spots.Count, 50);
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
            Assert.IsTrue(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_73b()
        {
            string line = "200712 1326   3 -27  0.6  14.097006  HB9FID JN47 37          0   306    0";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(7, spot.dt.Month);
            Assert.AreEqual(12, spot.dt.Day);
            Assert.AreEqual(13, spot.dt.Hour);
            Assert.AreEqual(26, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2007121326", spot.timestamp);
            Assert.AreEqual(-27, spot.strength);
            Assert.AreEqual(14.097006, spot.frequencyMHz);
            Assert.AreEqual(14_097_006, spot.frequencyHz);
            Assert.AreEqual("HB9FID", spot.callsign);
            Assert.AreEqual("JN47", spot.grid);
            Assert.IsTrue(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_73c()
        {
            string line = "200712 1322   1 -28 -1.1  14.097110  <PA0ANH> JO22LH 27     -1  5495   -2";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(7, spot.dt.Month);
            Assert.AreEqual(12, spot.dt.Day);
            Assert.AreEqual(13, spot.dt.Hour);
            Assert.AreEqual(22, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2007121322", spot.timestamp);
            Assert.AreEqual(-28, spot.strength);
            Assert.AreEqual(14.097110, spot.frequencyMHz);
            Assert.AreEqual(14_097_110, spot.frequencyHz);
            Assert.AreEqual("PA0ANH", spot.callsign);
            Assert.AreEqual("JO22LH", spot.grid);
            Assert.IsTrue(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_73d()
        {
            string line = "200712 1036   5 -27 -0.5  14.097014  <...> B000AA 63         0     2    0";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2020, spot.dt.Year);
            Assert.AreEqual(7, spot.dt.Month);
            Assert.AreEqual(12, spot.dt.Day);
            Assert.AreEqual(10, spot.dt.Hour);
            Assert.AreEqual(36, spot.dt.Minute);
            Assert.AreEqual(0, spot.dt.Second);
            Assert.AreEqual("2007121036", spot.timestamp);
            Assert.AreEqual(-27, spot.strength);
            Assert.AreEqual(14.097014, spot.frequencyMHz);
            Assert.AreEqual(14_097_014, spot.frequencyHz);
            Assert.IsNull(spot.callsign);
            Assert.AreEqual("B000AA", spot.grid);
            Assert.IsFalse(spot.isValid);
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
            Assert.IsTrue(spot.isValid);
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
            Assert.IsTrue(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_FT()
        {
            string line = "191011_130213    14.080 Rx FT4    -16  0.2 2012 UA4CCH YO9NC -07";
            var spot = new WsprSpot(line);

            Assert.AreEqual(2019, spot.dt.Year);
            Assert.AreEqual(10, spot.dt.Month);
            Assert.AreEqual(11, spot.dt.Day);
            Assert.AreEqual(13, spot.dt.Hour);
            Assert.AreEqual(2, spot.dt.Minute);
            Assert.AreEqual(13, spot.dt.Second);
            Assert.AreEqual("191011130213", spot.timestamp);
            Assert.AreEqual(-16, spot.strength);
            Assert.AreEqual(14.080, spot.frequencyMHz);
            Assert.AreEqual(14_080_000, spot.frequencyHz);
            Assert.AreEqual("UA4CCH", spot.callsign);
            Assert.AreEqual("YO9NC", spot.grid);
            Assert.IsTrue(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_garbage()
        {
            string line = "sdaf jldafj sdjfjas jfej alsjflk jes";
            var spot = new WsprSpot(line);

            Assert.IsFalse(spot.isValid);
        }

        [Test]
        public void Test_LogFormat_empty()
        {
            string line = "";
            var spot = new WsprSpot(line);

            Assert.IsFalse(spot.isValid);
        }
    }
}
