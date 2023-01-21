using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for AccountInformation.xaml
    /// </summary>
    public partial class AccountInformation : Page
    {
        public AccountInformation()
        {
            InitializeComponent();
            Globals.dbContext = new SmartBankDbContext();
            FindUser();
        }

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCustomer = new Customer
                {
                    Full_name = TbxFullName.Text,
                    Phone_number = TbxPhoneNumber.Text,
                    Password = TbxPassword.Text,
                    Email = TbxEmail.Text,
                    Address = TbxAddress.Text,
                    Created_at = DateTime.Now,
                    Account_type = TbxAccountType.Text
                };

                Globals.dbContext.Customers.Add(newCustomer);
                Globals.dbContext.SaveChanges();

                var newAccount = new Account
                {
                    Customer_id = newCustomer.Customer_id,
                    User_id = LoginPage.CurrentUser.users.User_id,
                    Bank_branch_address = "Smart Bank in Montreal",
                    Phone_number_branch = "4340000000"
                };
                Globals.dbContext.Accounts.Add(newAccount);
                Globals.dbContext.SaveChanges();

                ResetTbx();
                MessageBox.Show("Customer registration complete", "Customer registration", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Customer registration fail: " + ex.Message, "Customer registration", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public void FindUser()
        {
            Users user = LoginPage.CurrentUser.users;
            Customer customer = LoginPage.CurrentUser.customer;

            if (customer != null)
            {
                TbxFullName.Text = customer.Full_name;
                TbxEmail.Text = customer.Email;
                TbxPhoneNumber.Text = customer.Phone_number.ToString();
                TbxPassword.Text = customer.Password;
                TbxAddress.Text = customer.Address;
                TbxAccountType.Text = customer.Account_type;
                TbxAccountType.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;

                Account account = Globals.dbContext.Accounts.FirstOrDefault(c => c.Customer_id == customer.Customer_id);
                TbxAccountNumber.Text = account.Account_id.ToString();
            }
            else
            {
                BtnUpdate.Visibility = Visibility.Hidden;
            }
        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;

                var user = LoginPage.CurrentUser.users;

                var userInfoLines = new List<string>();

                if (user != null)
                {
                    userInfoLines.Add($"{user.Full_name};{user.Email};{user.Password};{user.Phone_number};{user.Account_type}");
                }

                var customer = LoginPage.CurrentUser.customer;

                if (customer != null)
                {
                    userInfoLines.Add($"{customer.Full_name};{customer.Email};{customer.Password};{customer.Phone_number};{customer.Account_type}");
                }
                File.WriteAllLines(fileName, userInfoLines);
                MessageBox.Show("File save successfully.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = LoginPage.CurrentUser.customer;
                Customer updateCustomer = Globals.dbContext.Customers.FirstOrDefault(c => c.Customer_id == customer.Customer_id);

                if (updateCustomer != null)
                {
                    updateCustomer.Full_name = TbxFullName.Text;
                    updateCustomer.Email = TbxEmail.Text;
                    updateCustomer.Phone_number = TbxPhoneNumber.Text;
                    updateCustomer.Password = TbxPassword.Text;
                    updateCustomer.Address = TbxAddress.Text;
                    updateCustomer.Created_at = DateTime.Now;
                    updateCustomer.Account_type = TbxAccountType.Text;

                    Globals.dbContext.SaveChanges();
                    MessageBox.Show("User updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Customer update fail: " + ex.Message, "Customer update", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetTbx()
        {
            TbxFullName.Text = "";
            TbxEmail.Text = "";
            TbxPhoneNumber.Text = "";
            TbxPassword.Text = "";
            TbxAddress.Text = "";
            TbxAccountNumber.Text = "";
        }
    }
}
