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
    /// Interaction logic for Calculator.xaml
    /// </summary>


    public partial class Calculator : Page
    {
        const string Placeholder = "Enter amount...";
        public List<string> OfferList { get; set; } = new List<string>()
        {
            "3 Year Fixed Rate Mortgage 3%",
            "5 Year Fixed Rate Mortgate 3.5%"
        };

        public List<string> FixedRateList { get; set; } = new List<string>()
        {
            "1 Year Closed 7%",
            "2 Year Closed 5%",
            "3 Year Closed 5%",
            "4 Year Closed 5.5%",
            "5 Year Closed 6%",
            "6 Year Closed 6.2%",
            "7 Year Closed 6.4%",
            "8 Year Closed 6.6%",
            "9 Year Closed 6.8%",
            "10 Year Closed 7%"
        };

        public List<string> PaymentFrequencyList { get; set; } = new List<string>()
        {
            "Weekly",
            "Bi-Weekly",
            "Monthly"
        };


        public Calculator()
        {
            InitializeComponent();

            AddRateListAll();
            AddPeriodList();
            AddPatmentFrequency();

            TbxMortgageAmount.Text = Placeholder;
            TbxMortgageAmount.GotFocus += RemovePlaceholder;
            TbxMortgageAmount.LostFocus += SetPlaceholder;
        }



        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = Placeholder;
            }
        }

        private void AddRateListAll()
        {
            //DataContext = new RateList();

            ComboboxRate.Items.Add(new ComboBoxItem()
            {
                Content = "Spacial Offers",
                IsEnabled = false,
            });

            OfferList.ForEach(offer =>
            {
                ComboboxRate.Items.Add(offer);
            });
            ComboboxRate.Items.Add(new Separator());

            ComboboxRate.Items.Add(new ComboBoxItem()
            {
                Content = "Fixed Rate Mortgages",
                IsEnabled = false,
            });

            FixedRateList.ForEach(fixedRate =>
            {
                ComboboxRate.Items.Add(fixedRate);
            });
        }

        private void AddPeriodList()
        {
            ComboboxPreiod.Items.Add("1 year");
            for (int i=2; i<=25; i++)
            {
                ComboboxPreiod.Items.Add(i + " years");
            }
        }

        private void AddPatmentFrequency()
        {
            PaymentFrequencyList.ForEach(frequency =>
            {
                ComboboxPayFreq.Items.Add(frequency);
            });
        }


    }
}
