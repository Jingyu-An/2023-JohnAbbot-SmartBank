using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
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
using MaterialDesignThemes.Wpf;

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {

        public LoginPage()
        {
            InitializeComponent();
            Globals.dbContext = new SmartBankDbContext();
        }

        public bool IsDarktheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public void ToggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarktheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarktheme = false;
                theme.SetBaseTheme(Theme.Light);

            }
            else
            {
                IsDarktheme = true;
                theme.SetBaseTheme(Theme.Dark);

            }
            paletteHelper.SetTheme(theme);
        }

        public void ExitApp(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Password;

            Users user = Globals.dbContext.UserEmployees.FirstOrDefault(u => u.Full_name == username && u.Password == password);
            Customer customer = Globals.dbContext.Customers.FirstOrDefault(u => u.Full_name == username && u.Password == password);
            MainWindow mainWindow = new MainWindow();
            if (user != null)
            {
                CurrentUser.users = user;

                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                this.Close();
            }
            else if (customer != null)
            {
                CurrentUser.customer = customer;
                var result = (from accounts in Globals.dbContext.Accounts
                              where accounts.Customer_id == customer.Customer_id
                              join users in Globals.dbContext.UserEmployees
                              on accounts.User_id equals users.User_id into u
                              from users in u
                              select users).FirstOrDefault();

                CurrentUser.users = result;

                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }


        public static class CurrentUser
        {
            public static Users users { get; set; }
            public static Customer customer { get; set; }
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            Signup signup = new Signup();
            Application.Current.MainWindow = signup;
            signup.Show();
            this.Close();
        }
    }
}
