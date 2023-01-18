using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Operation
    {
        public int account_id { get; set; }
        public int deposit_amount { get; set; }
        public int withdrawal_amount { get; set; }
        public int other_acount_id { get; set; }
        public DateTime date_operation { get; set; }
        public string description { get; set; }
        
        public enum transfer_type_enum { Cash = 0, Check=1}
        [EnumDataType(typeof(transfer_type_enum))]
        public transfer_type_enum transfer_Type { get; set; }

    }
}
