using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    class Currency
    {

        public string Unit { get; set; }
        public string CountryName { get; set; }
        public string TTB { get; set; }
        public string TTS { get; set; }
        public string Deal { get; set; }

        public Currency(string unit, string countryName, string tTB, string tTS, string deal)
        {
            Unit = unit;
            CountryName = countryName;
            TTB = tTB;
            TTS = tTS;
            Deal = deal;
        }
    }
}
