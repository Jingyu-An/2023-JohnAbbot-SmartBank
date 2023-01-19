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
        public Operation(int deposit_amount, int withdrawal_amount, int other_account_id, string description, transfer_type_enum transfer_Type)
        {
            Account_id = 0;
            Deposit_amount = deposit_amount;
            Withdrawal_amount = withdrawal_amount;
            Other_account_id = other_account_id;
            Description = description;
            Transfer_Type = transfer_Type;
            Date_operation = DateTime.Now;
        }

        
        public int Account_id { get; set; }
        public int Deposit_amount { get; set; }
        public int Withdrawal_amount { get; set; }
        public int Other_account_id { get; set; }
        public DateTime Date_operation { get; set; }
        public string Description { get; set; }

        public enum transfer_type_enum { Cash = 0, Check = 1 }
        [EnumDataType(typeof(transfer_type_enum))]
        public transfer_type_enum Transfer_Type { get; set; }

    }
}