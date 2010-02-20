using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TronBot
{
    public class Debug
    {
        public const bool debug = true;
        public const string path = "log.txt";
        public static void reset()
        {
            if (!debug)         //So doesn't try debugging on tournement server
                return;
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public static void log(string toLog)
        {
            if (!debug)         //So doesn't try debugging on tournement server
                return;
            try
            {
                File.AppendAllText(path, "\r\n" + toLog);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}
