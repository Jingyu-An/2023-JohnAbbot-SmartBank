using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Users
    {
        public Users() { }

        public Users(string email, string full_name, int phone_number, string password, string address, DateTime created_at, string account_type)
        {
            Email = email;
            Full_name = full_name;
            Phone_number = phone_number;
            Password = password;
            Address = address;
            this.created_at = created_at;
            Account_type = account_type;
        }

        string Email { get; set; }
        string Full_name { get; set; }
        int Phone_number { get; set; }
        string Password { get; set; }
        string Address { get; set; }
        DateTime created_at { get; set; }
        string Account_type { get; set; }

    }
}
