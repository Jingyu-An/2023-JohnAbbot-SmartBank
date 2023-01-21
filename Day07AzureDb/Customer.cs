using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Customer
    {
        public Customer () { }

        public Customer(string email, string full_name, string phone_number, string password, string address, DateTime created_at, string account_type)
        {
            Email = email;
            Full_name = full_name;
            Phone_number = phone_number;
            Password = password;
            Address = address;
            Created_at = created_at;
            Account_type = account_type;
        }

        [Key]
        public int Customer_id { get; set; }
        public string Email { get; set; }
        public string Full_name { get; set; }
        public string Phone_number { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime Created_at { get; set; }
        public string Account_type { get; set; }
    }
}
