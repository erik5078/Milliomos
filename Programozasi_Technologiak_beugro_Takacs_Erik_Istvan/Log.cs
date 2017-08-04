using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wpf_beadando
{
    class Log
    {
        private StreamWriter iro;
        private static Log instance;

        public static Log Instance()
        {
            if (instance == null)
            {
                instance = new Log();
            }

            return instance;
        }

        private Log()
        {

        }

        public void Write(string szoveg)
        {
            iro = new StreamWriter("log.txt", true);
            iro.WriteLine(DateTime.Now + ";" + szoveg);
            iro.Close();
        }
    }
}
