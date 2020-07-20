using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGuardDesktop
{
    class Utils
    {
        public static long GetSystemUnixTime()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string FileRead(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string tmp = string.Empty;
            try
            {
                tmp = sr.ReadToEnd();
            }
            catch (Exception)
            {
                
            }
            sr.Close();
            return tmp;
        }

        public static bool FileWrite(string fileName, string content)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                sw.Write(content);
            }
            catch (Exception)
            {
                sw.Close();
                return false;
            }
            sw.Close();
            return true;
        }
    }

}
