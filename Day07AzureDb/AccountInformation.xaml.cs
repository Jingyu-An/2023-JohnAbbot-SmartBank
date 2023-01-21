using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
                using (Globals.dbContext)
                {
                    var newCustomer = new Customer
                    {
                        Full_name = TbxFullName.Text,
                        Phone_number = TbxPhoneNumber.Text,
                        Password = TbxPassword.Text,
                        Email = TbxEmail.Text,
                        Address = TbxAddress.Text,
                        Created_at = DateTime.Now,
                        Account_type = TbxAccountNumber.Text
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

                    //dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Users]");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Customer registration fail: " + ex.Message, "Customer registration", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public void FindUser()
        {
            //Globals.dbContext = new SmartBankingDbContext();
            // Using LINQ
            using (Globals.dbContext)
            {
                Users user = LoginPage.CurrentUser.users;
                Customer customer = LoginPage.CurrentUser.customer;
                /*                var user = (from userOne in Globals.dbContext.UserEmployees
                                            where userOne.User_id == 1
                                            select userOne).FirstOrDefault();
                */
                if (customer != null)
                {
                    TbxFullName.Text = customer.Full_name;
                    TbxEmail.Text = customer.Email;
                    TbxPhoneNumber.Text = customer.Phone_number.ToString();
                    TbxPassword.Text = customer.Password;
                    TbxAddress.Text = customer.Address;
                    TbxAccountNumber.Text = customer.Account_type;
                    TbxAccountNumber.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                }
                else
                {
                    BtnUpdate.Visibility = Visibility.Hidden;
                }

            }


        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt file (*.txt)|*.txt|All files (*.*)|*.*";
            //saveFileDialog.FilterIndex = 1;
            //saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultExt = "txt";
            //saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Globals.dbContext)
                {
                    string fileName = saveFileDialog.FileName;
                    var user = (from userOne in Globals.dbContext.UserEmployees
                                where userOne.User_id == 1
                                select userOne).FirstOrDefault();

                    var userInfoLines = new List<string>();

                    if (user != null)
                    {
                        userInfoLines.Add($"{user.Full_name};{user.Email};{user.Password};{user.Phone_number};{user.Account_type}");
                    }
                    File.WriteAllLines(fileName, userInfoLines);
                    MessageBox.Show("File save successfully.");
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (Globals.dbContext)
            {
                var user = (from userOne in Globals.dbContext.UserEmployees
                            where userOne.User_id == 1
                            select userOne).FirstOrDefault();

                if (user != null)
                {
                    user.Full_name = TbxFullName.Text;
                    user.Email = TbxEmail.Text;
                    user.Phone_number = TbxPhoneNumber.Text;
                    user.Password = TbxPassword.Text;
                    user.Address = TbxAddress.Text;
                    user.Account_type = TbxAccountNumber.Text;

                    Globals.dbContext.SaveChanges();
                    MessageBox.Show("User updated successfully");
                }
                else
                {
                    MessageBox.Show("User not found");
                }
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
