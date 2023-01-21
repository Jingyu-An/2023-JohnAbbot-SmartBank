using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
using static Day07AzureDb.Operation;

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        public Transactions()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new SmartBankDbContext(); // Exceptions
                FindUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Operation operations = new Operation(0, 0, 0, "text", 0);
            Globals.dbContext.Operations.Add(operations);
            Globals.dbContext.SaveChanges();
            MessageBox.Show("Send money successfully!");
        }

        public void FindUser()
        {
            Users user = LoginPage.CurrentUser.users;
            Customer customer = LoginPage.CurrentUser.customer;

            if (customer != null)
            {
                LblName.Content = customer.Full_name;

                var result = from accounts in Globals.dbContext.Accounts
                             where accounts.Customer_id == customer.Customer_id
                             select new { accountID = accounts.Account_id };

                foreach (var account in result)
                {
                    ComboBoxAccounts.Items.Add(account.accountID);
                }


            }
            else if (user != null)
            {
                LblName.Content = user.Full_name + " (Employee)";
                ComboBoxAccounts.IsEnabled = false;
                TbxAmount.IsEnabled = false;
                TbxRecipient.IsEnabled = false;
                TbxDesc.IsEnabled = false;
                BtnSend.IsEnabled = false;
            }
        }
        private void ComboBoxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox accounts = (ComboBox)sender;

            if (accounts != null)
            {
                int selectedAccount = int.Parse(accounts.SelectedItem.ToString());
                LblCurrentBalance.Content = Globals.dbContext.Accounts.FirstOrDefault(a => a.Account_id == selectedAccount).Account_balance;
            }

        }

    }
}