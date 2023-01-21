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

        public Users(string email, string full_name, string phone_number, string password, string address, DateTime created_at, string account_type, int account_balance)
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
        [Required]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]+[a-zA-Z]+$"))
                {
                    throw new FormatException("Please insert a valid email address.");
                }
                _email = value;

            }
        }

        private string _full_name;
        [Required]
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
                    throw new FormatException("Invalid inputs, please insert letters only.");
                }
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new FormatException("Minimum and/or maximum character length exceeded(2-30).");
                }
                _full_name = value;
            }
        }

        private string _phone_number;
        [Required]
        public string Phone_number
        {
            get
            {
                return _phone_number;

            }
            set
            {
                if (!Regex.IsMatch(value, @"^\d{10}"))
                {
                    throw new FormatException("Please insert 10 numbers, no dashes please.");
                }
                _phone_number = value;
            }
        }
        private string _password;
        [Required]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[\d\w]{5,10}$"))
                {
                    throw new FormatException("Please insert a password between 5 to 10 characters long");
                }
                _password = value;
            }
        }
        private string _address;
        [Required]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^\d{1,10}\s+[a-zA-Z]+$"))
                {
                    throw new FormatException("Please start with street number and ");
                }
                _address = value;
            }
        }
        public DateTime Created_at { get; set; }
        public string Account_type { get; set; }

    }

}
