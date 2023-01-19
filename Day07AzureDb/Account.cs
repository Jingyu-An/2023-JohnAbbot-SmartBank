using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Account
    {
        public Account() { }

        public Account(int customer_id, int user_id, string bank_branch_address, string phone_number_branch)
        {
            Customer_id = customer_id;
            User_id = user_id;
            Bank_branch_address = bank_branch_address;
            Phone_number_branch = phone_number_branch;
        }

        int Id { get; set; }
        
        public int Customer_id { get; set; }
        public int User_id { get; set; }
        public string Bank_branch_address{ get; set; }
        public string Phone_number_branch { get; set; }



    }
}
