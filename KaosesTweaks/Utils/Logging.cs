using System;
using System.IO;

namespace KaosesTweaks.Utils
{
    class Logging
    {
        public static string PrePrend = "";

        public static void Lm(string message)
        {
            try
            {
                using StreamWriter sw = File.AppendText(Statics.LogPath);
                sw.WriteLine(PrePrend + " : " + DateTime.Now.ToString() + " : " + message + "\r\n");
            }
            catch
            {

            }
        }

    }
}
