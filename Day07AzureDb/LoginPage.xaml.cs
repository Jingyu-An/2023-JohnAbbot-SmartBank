using System;
using System.Collections.Generic;
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

            using (var dbContext = new SmartBankingDbContext())
            {
                var user = dbContext.UserEmployees.FirstOrDefault(u => u.Full_name == username && u.Password == password);
                MainWindow mainWindow = new MainWindow();
                if (user != null)
                {
                    CurrentUser.users = user;

                    // how to be directed to mainwindow?
                   
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                   
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }
      

        public static class CurrentUser
        {
            public static Users users { get; set; }
        }
    }
}
