using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Operation
    {
        public static int _transaction_id = 1;

        public Operation()
        {
            Transaction_id = _transaction_id++;
        }

      /*
        public Operation(int deposit_amount, int withdrawal_amount, int other_account_id, string description, transfer_type_enum transfer_Type)
        {
            Deposit_amount = deposit_amount;
            Withdrawal_amount = withdrawal_amount;
            Other_account_id = other_account_id;
            Description = description;
            Transfer_Type = transfer_Type;
            Date_operation = DateTime.Now;
            Transaction_id = _transaction_id++;
        }
        */
        [Key]
        public int Transaction_id { get; set; }
        public int Deposit_amount { get; set; }
        public int Withdrawal_amount { get; set; }
        public int Other_account_id { get; set; }
        public DateTime Date_operation { get; set; }
        public string Description { get; set; }

        public enum transfer_type_enum { Cash = 0, Check = 1 }
        [EnumDataType(typeof(transfer_type_enum))]
        public transfer_type_enum Transfer_Type { get; set; }

     
        // Foreign key   
        [Display(Name = "Account")]
        public virtual int Account_id { get; set; }

        [ForeignKey("Account_id")]
        public virtual Account Accounts { get; set; }

    }
}