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
        public static int selectedAccount;
        Account accountRecipient;
        Account accountSender;
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
            int amount = int.Parse(TbxAmount.Text);
            int balance = int.Parse(LblCurrentBalance.Content.ToString());

            if (amount > balance)
            {
                MessageBox.Show("Balance is insufficient.");
            }
            else if (!FindRecipient())
            {
                MessageBox.Show("The recipient could not be found.");
            }
            else
            {
                var newOperations = new Operation
                {
                    Withdrawal_amount = amount,
                    Date_operation = DateTime.Now,
                    Account_id = selectedAccount,
                    Description = TbxDesc.Text,
                    Other_account_id = accountRecipient.Customer_id
                };
          
                accountRecipient.Account_balance = (int.Parse(accountRecipient.Account_balance) + amount).ToString();
                
                accountSender.Account_balance = (balance - amount).ToString();
               
            
                Globals.dbContext.Operations.Add(newOperations);
             
                Globals.dbContext.SaveChanges();

                MessageBox.Show("Send money successfully!");
                LblCurrentBalance.Content = accountSender.Account_balance;
            }

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
        public bool FindRecipient()
        {

            int recipeint = int.Parse(TbxRecipient.Text);

            accountRecipient = Globals.dbContext.Accounts.FirstOrDefault(a => a.Account_id == recipeint);

            if (accountRecipient.Account_id.ToString() == null)
            {
                return false;
            }

            return true;
        }

        private void ComboBoxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox accounts = (ComboBox)sender;
            if (accounts != null)
            {
                selectedAccount = int.Parse(accounts.SelectedItem.ToString());
                accountSender = Globals.dbContext.Accounts.FirstOrDefault(a => a.Account_id == selectedAccount);
                LblCurrentBalance.Content = accountSender.Account_balance;
            }

        }

    }
}