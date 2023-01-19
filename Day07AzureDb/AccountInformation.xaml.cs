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
            FindUser();
        }

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new SmartBankingDbContext())
            {
                var newUser = new Users
                {

                    Full_name = "John Smith",
                    Phone_number = 514888988,
                    Password = "password",
                    Email = "testing.com",
                    Address = "Montreal ave",
                    Created_at = DateTime.Now,
                    Account_type = "Checking"
                };
                dbContext.UserEmployees.Add(newUser);
                dbContext.SaveChanges();

                //dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Users]");
            }

        }

        public void FindUser()
        {
            //Globals.dbContext = new SmartBankingDbContext();
            // Using LINQ
            using (var dbSmartDb = new SmartBankingDbContext())
            {
                var user = (from userOne in dbSmartDb.UserEmployees
                            where userOne.Id == 1
                            select userOne).FirstOrDefault();

                if (user != null)
                {
                    TbxFullName.Text = user.Full_name;
                    TbxEmail.Text = user.Email;
                    TbxPhoneNumber.Text = user.Phone_number.ToString();
                    TbxPassword.Text = user.Password;
                    TbxAddress.Text = user.Address;
                    TbxAccountNumber.Text = user.Account_type;
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
                using (var dbSmartDb = new SmartBankingDbContext())
                {
                    string fileName = saveFileDialog.FileName;
                    var user = (from userOne in dbSmartDb.UserEmployees
                                where userOne.Id == 1
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

        private void BtnUpdate_click(object sender, RoutedEventArgs e)
        {
            using (var dbSmartDb = new SmartBankingDbContext())
            {
                var user = (from userOne in dbSmartDb.UserEmployees
                            where userOne.Id == 1
                            select userOne).FirstOrDefault();

                if (user != null)
                {
                    user.Full_name = TbxFullName.Text;
                    user.Email = TbxEmail.Text;
                    user.Phone_number = int.Parse(TbxPhoneNumber.Text);
                    user.Password = TbxPassword.Text;
                    user.Address = TbxAddress.Text;
                    user.Account_type = TbxAccountNumber.Text;

                    dbSmartDb.SaveChanges();
                    MessageBox.Show("User updated successfully");
                }
                else
                {
                    MessageBox.Show("User not found");
                }
            }
        }
    }
}
