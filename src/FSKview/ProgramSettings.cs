﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FSKview
{
    public class ProgramSettings
    {
        public string version;

        public int audioDeviceIndex = 0;
        public string window = "Cosine";
        public string colormap = "Viridis";
        public int dialFrequencyIndex = 5;
        public double brightness = 2.0;
        public bool isWsprEnabled = true;
        public string wsprLogFilePath = null;
        public int wsprBandIndex = 7;
        public bool showBands = true;
        public bool saveGrabs = true;
        public bool showScaleOnAllGrabs = false;
        public string grabFileName = "latest.png";
        public bool roll = true;
        public bool showTimeLines = false;

        public int targetWidth = 1000;
        public int grabSavePxAbove = 120;
        public int grabSavePxBelow = 170;
        public int verticalReduction = 2;
        public int freqDisplayOffset = 0;
        public string stationInformation = "station information not set";

        public string ftpServerAddress = "ftp://ftp.qsl.net";
        public string ftpRemoteSubfolder = "/";
        public string ftpUsername = "sampleUsername";
        public string ftpObfuscatedPassword = "c2FtcGxlUGFzc3dvcmQ";
        public bool isFtpEnabled = false;
        public int agcMode = 0;
        public double agcPower = 1;

        public ProgramSettings()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.version = $"{version.Major}.{version.Minor}";

            string predictedWsprFilePath = Path.GetFullPath(Application.LocalUserAppDataPath + "../../../../WSJT-X/ALL_WSPR.TXT");
            wsprLogFilePath = File.Exists(predictedWsprFilePath) ? predictedWsprFilePath : null;
        }

        public static ProgramSettings Load(string filePath = "settings.xml")
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string expectedVersion = $"{version.Major}.{version.Minor}";

            XmlSerializer reader = new XmlSerializer(typeof(ProgramSettings));
            using (StreamReader file = new StreamReader(filePath))
            {
                ProgramSettings loadedConfig = (ProgramSettings)reader.Deserialize(file);
                if (loadedConfig.version == expectedVersion)
                    return loadedConfig;
                else
                    throw new InvalidOperationException("incompatible config file version");
            }
        }

        public void Save(string filePath = "settings.xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
            using (StreamWriter fileWriter = new StreamWriter(filePath))
            {
                serializer.Serialize(fileWriter, this);
            }
        }

        public string GetXML()
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public string Obfuscate(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message)).Trim('=');
        }

        public string DeObfuscate(string obfuscated)
        {
            while (obfuscated.Length % 4 != 0)
                obfuscated += "=";
            return Encoding.UTF8.GetString(Convert.FromBase64String(obfuscated));
        }
    }
}
