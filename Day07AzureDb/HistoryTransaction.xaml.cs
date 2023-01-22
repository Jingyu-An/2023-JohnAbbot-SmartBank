using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for HistoryTransaction.xaml
    /// </summary>
    public partial class HistoryTransaction : Page
    {
        Account accountSender;
        public static int selectedAccount;
        List<Operation> operationList = new List<Operation>();

        public ChartValues<int> Values { get; set; }


        public HistoryTransaction()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new SmartBankDbContext(); // Exceptions
                FindUser();
                HistoryLv.ItemsSource = operationList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (accountSender != null)
            {
                var result = (from operations in Globals.dbContext.Operations
                              where operations.Account_id == accountSender.Account_id
                              select operations).AsParallel().ToList();

                HistoryLv.ItemsSource = result;
                HistoryLv.Items.Refresh();
                try
                {
                    Values = new ChartValues<int>();
                    result.ForEach(o =>
                    {
                        {
                            Values.Add(o.Withdrawal_amount);
                        };
                    });

                    DataContext = this;
                }
                catch (Exception ex) when (ex is NullReferenceException)
                {
                    MessageBox.Show("null");
                }
            }

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