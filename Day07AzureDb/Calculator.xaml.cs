using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>


    public partial class Calculator : Page
    {
        const int Placeholder = 10000;
        private int Amount = 0;
        private float rate = 0;
        private float year = 1;
        private float freq = 1;
        public List<string> OfferRateList { get; set; } = new List<string>()
        {
            "3 Year Fixed Rate Mortgage 3.0%",
            "5 Year Fixed Rate Mortgate 3.5%"
        };

        public List<string> FixedRateList { get; set; } = new List<string>()
        {
            "1 Year Closed 7.0%",
            "2 Year Closed 5.0%",
            "3 Year Closed 5.0%",
            "4 Year Closed 5.5%",
            "5 Year Closed 6.0%",
            "6 Year Closed 6.2%",
            "7 Year Closed 6.4%",
            "8 Year Closed 6.6%",
            "9 Year Closed 6.8%",
            "10 Year Closed 7.0%"
        };

        public Dictionary<string, float> RateMap = new Dictionary<string, float>();
        public Dictionary<string, int> PreiodMap = new Dictionary<string, int>();

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

            TbxMortgageAmount.Text = Placeholder.ToString();
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
                txt.Text = Placeholder.ToString();
            }
        }

        private void AddRateListAll()
        {
            //DataContext = new RateList();
            Regex regex = new Regex("([0-9].[0-9])");
            try
            {
                ComboboxRate.Items.Add(new ComboBoxItem()
                {
                    Content = "Spacial Offers",
                    IsEnabled = false,
                });

                OfferRateList.ForEach(offer =>
                {
                    Match match = regex.Match(offer);
                    ComboboxRate.Items.Add(offer);
                    RateMap.Add(offer,float.Parse(match.ToString(), CultureInfo.InvariantCulture.NumberFormat));
                });
                ComboboxRate.Items.Add(new Separator());

                ComboboxRate.Items.Add(new ComboBoxItem()
                {
                    Content = "Fixed Rate Mortgages",
                    IsEnabled = false,
                });

                FixedRateList.ForEach(fixedRate =>
                {
                    Match match = regex.Match(fixedRate);
                    ComboboxRate.Items.Add(fixedRate);
                    RateMap.Add(fixedRate, float.Parse(match.ToString(), CultureInfo.InvariantCulture.NumberFormat));
                });
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid Amount value" + ex.Message, "Fromat error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPeriodList()
        {
            ComboboxPreiod.Items.Add("1 year");
            PreiodMap.Add("1 years", 1);
            for (int i=2; i<=25; i++)
            {
                ComboboxPreiod.Items.Add($"{i} years");
                PreiodMap.Add($"{i} years", i);
            }
        }

        private void AddPatmentFrequency()
        {
            PaymentFrequencyList.ForEach(frequency =>
            {
                ComboboxPayFreq.Items.Add(frequency);
            });
        }

        private void TbxMortgageAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            try
            {
                if (txt.Text != "")
                {
                    Amount = int.Parse(txt.Text);
                    TextblockMortgageAmount.Text = Amount.ToString("C");
                    //PaymentAmountCalculator(sender);
                }

            } 
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid Amount value", "Fromat error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TbxMortgageAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PaymentAmountCalculator(object sender)
        {
            ComboBox com = (ComboBox)sender;
            double payment = 0;

            string rateItem = "";
            string preiodItem = "";
            string freqItem = "";

            if (com.Name == "ComboboxRate") {
                rateItem = com.SelectedItem.ToString();
            }
            if (com.Name == "ComboboxPreiod")
            {
                preiodItem = com.SelectedItem.ToString();
            }

            if (com.Name == "ComboboxPayFreq")
            {
                freqItem = com.SelectedItem.ToString();
                if(freqItem == "Monthly")
                {
                    freq = 1;
                } else if (freqItem == "Weekly")
                {
                    freq = 4;
                } else if (freqItem == "Bi-Weekly")
                {
                    freq = 2;
                }
            }

            if (rateItem != null && RateMap.ContainsKey(rateItem))
            {
                rate = RateMap[rateItem]/100;
            }
            if (preiodItem != null && PreiodMap.ContainsKey(preiodItem))
            {
                year = PreiodMap[preiodItem];
            }

            
            payment = Amount * ((rate * Math.Pow((1 + rate), year) / (Math.Pow((1 + rate), year) - 1))) /12 / freq;

            TextblockPaymentAmount.Text = payment.ToString("C");
        }

        private void ComboboxRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PaymentAmountCalculator(sender);
        }
    }
}
