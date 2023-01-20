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
                Globals.dbContext = new SmartBankingDbContext(); // Exceptions
                Operation operations = new Operation(0, 0, 0, "text", 0);
                Globals.dbContext.Operations.Add(operations);
                Globals.dbContext.SaveChanges();

              
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Send money successfully!");
        }

    }
}
