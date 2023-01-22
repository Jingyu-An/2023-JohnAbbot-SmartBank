using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    internal class Globals
    {
        static internal SmartBankDbContext dbContext;
        public static Users users { get; set; }
        public static Customer customer { get; set; }
    }
}
