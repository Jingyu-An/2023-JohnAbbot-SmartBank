using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        private string EmployeeCode = "SmartBank";

        public Signup()
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

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TbxCode.Text != EmployeeCode)
            {
                MessageBox.Show("Invalid Employee Code", "Employee registration", MessageBoxButton.OK, MessageBoxImage.Information);
                TbxCode.Focus();
            }
            else
            {
                try
                {
                    var newUser = new Users
                    {
                        Full_name = TbxUsername.Text,
                        Phone_number = TbxPhone.Text,
                        Password = TbxPassword.Password,
                        Email = TbxEmail.Text,
                        Address = TbxAddress.Text,
                        Created_at = DateTime.Now,
                        Account_type = "Employee"
                    };

                    Globals.dbContext.UserEmployees.Add(newUser);
                    Globals.dbContext.SaveChanges();
                    MessageBox.Show("Employee registration complete", "Employee registration", MessageBoxButton.OK, MessageBoxImage.Information);

                    BackToLoginPage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Employee registration fail: " + ex.Message, "Employee registration", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            BackToLoginPage();
        }

        private void BackToLoginPage()
        {
            LoginPage loginPage = new LoginPage();
            Application.Current.MainWindow = loginPage;
            loginPage.Show();
            this.Close();
        }

        private void TbxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
