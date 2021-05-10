using System;
using System.Collections.Generic;

namespace webScrapingMultiprocessing
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            while (true)
            {
                ScrapingMultiprocessingFunctions mp = new ScrapingMultiprocessingFunctions();
                mp.ScrapingMultiproccesing();
            }
        }
    }
}