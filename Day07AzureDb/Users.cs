using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07AzureDb
{
    public class Users
    {
        public Users() { }

        public Users(string email, string full_name, string phone_number, string password, string address, DateTime created_at, string account_type)
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
        public int User_id { get; set; }
        private string _email;
        
        public string Email
        {
            get; set;

        }

        private string _full_name;
        public string Full_name
        {
            get
            {
                return _full_name;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
                {
                    throw new ArgumentException("Invalid inputs, please insert letters only.");
                }
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new ArgumentException("Minimum and/or maximum character length exceeded(2-30).");
                }
                _full_name = value;
            }
        }
        public string Phone_number { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime Created_at { get; set; }
        public string Account_type { get; set; }

    }
    
}
