using NUnit.Framework;
using System;
using System.IO;

namespace FSKview.Tests
{
    public class TestProgramSettings
    {
        public void Test_Settings_SaveAndLoad()
        {
            var settings = new ProgramSettings();
            Assert.AreEqual(2.0, settings.brightness);
            Assert.AreEqual("Viridis", settings.colormap);

            double newBrightness = 1.234;
            string newColormap = "Turbo";
            settings.brightness = newBrightness;
            settings.colormap = newColormap;
            settings.Save("testSettings.xml");

            var settings2 = ProgramSettings.Load("testSettings.xml");
            Assert.AreEqual(newBrightness, settings2.brightness);
            Assert.AreEqual(newColormap, settings2.colormap);

            Console.WriteLine(settings2.GetXML());
        }
    }
}