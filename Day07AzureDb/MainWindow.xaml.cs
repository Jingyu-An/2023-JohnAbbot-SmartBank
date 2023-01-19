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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToMiniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void ToMaxOrNormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
