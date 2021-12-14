using System;
using System.IO;
using System.Net;

#pragma warning disable SYSLIB0014 // Ignore obsolete WebClient

namespace FSKview
{
    public static class FTP
    {
        public static string Upload(string serverAddress, string remoteFolder, string username, string password, string localPath)
        {
            string prefix = serverAddress.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase) ? "" : "ftp://";
            string uploadFolder = $"{prefix}{serverAddress}/{remoteFolder}/";

            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(username, password);
                    string uploadPath = uploadFolder + Path.GetFileName(localPath);
                    client.UploadFile(uploadPath, WebRequestMethods.Ftp.UploadFile, localPath);
                    return "upload successful";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
