using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Json;
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
using System.Xml;

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for CurrencyRate.xaml
    /// </summary>
    public partial class CurrencyRate : Page
    {
        List<Currency> currencyList = new List<Currency>();
        public static double cadttb = 0;
        public static double cadtts = 0;
        public static double cadDeal = 0;

        public static double krwttb = 0;
        public static double krwtts = 0;
        public static double krwDeal = 0;

        public static double usttb = 0;
        public static double ustts = 0;
        public static double usDeal = 0;

        public List<string> CurrencyFromList { get; set; } = new List<string>()
        {
            "CANADA - CAD",
            "US     - USD",
            "KOREA  - KRW",

        };
        public List<string> CurrencyToList { get; set; } = new List<string>()
        {

            "US     - USD",
            "KOREA  - KRW",
            "CANADA - CAD",

        };

        public CurrencyRate()
        {
            InitializeComponent();
            AddCountryListAll();
            CurrencyLv.ItemsSource = currencyList;
        }

        private void AddCountryListAll()
        {

            CurrencyFromList.ForEach(currencyFrom =>
            {
                ComboboxFrom.Items.Add(currencyFrom);
            });

            CurrencyToList.ForEach(currencyTo =>
            {
                ComboboxTo.Items.Add(currencyTo);
            });

        }

        private bool StringToJson(string strTmp, ref string strErr)
        {
            if (strTmp.Length < 3)
            {
                strErr = "Sorry, Service is closed.";
                return false;
            }

            JsonTextParser jtr = new JsonTextParser();
            JsonObject jo = jtr.Parse(strTmp);
            JsonArrayCollection jac = (JsonArrayCollection)jo;

            int iRow = 0;
            foreach (JsonObjectCollection joc in jac)
            {
                iRow += 1;
                JsonDataSet(iRow, joc);
            }


            return true;
        }

        private void JsonDataSet(int iRow, JsonObjectCollection joc)
        {

            string unit = "";
            string countryName = "";
            string ttb = "";
            string tts = "";
            string deal = "";

            if (joc["result"].ToString().Contains("1"))
            {
                unit = joc["cur_unit"].ToString().Split(':')[1].Replace('"', ' ').Trim();
                countryName = joc["cur_nm"].ToString().Split(':')[1].Replace('"', ' ').Trim();
                ttb = joc["ttb"].ToString().Split(':')[1].Replace('"', ' ').Trim();
                tts = joc["tts"].ToString().Split(':')[1].Replace('"', ' ').Trim();
                deal = joc["deal_bas_r"].ToString().Split(':')[1].Replace('"', ' ').Trim();
            }

            bool us = unit.Equals("USD");
            bool kor = unit.Equals("KRW");
            bool cad = unit.Equals("CAD");

            if (us || kor || cad)
            {
                if (cad)
                {
                    countryName = countryName.Replace("캐나다 달러", "CANADA");

                    cadttb = (Double.Parse(ttb));
                    cadtts = (Double.Parse(tts));
                    cadDeal = (Double.Parse(deal));

                    ttb = ttb.Replace(ttb, "0");
                    tts = tts.Replace(tts, "0");
                    deal = deal.Replace(deal, "1");

                }
                else if (kor)
                {
                    countryName = countryName.Replace("한국 원", "KOREA");

                    krwttb = (Double.Parse(ttb));
                    krwtts = (Double.Parse(tts));
                    krwDeal = (Double.Parse(deal));

                    ttb = ttb.Replace(ttb, cadttb.ToString());
                    tts = tts.Replace(tts, cadtts.ToString());
                    deal = deal.Replace(deal, cadDeal.ToString());
                }
                else if (us)
                {
                    countryName = countryName.Replace("미국 달러", "US");

                    usttb = (Double.Parse(ttb));
                    ustts = (Double.Parse(tts));
                    usDeal = (Double.Parse(deal));

                    usttb = cadttb / usttb;
                    ustts = cadtts / ustts;
                    usDeal = cadDeal / usDeal;

                    ttb = ttb.Replace(ttb, usttb.ToString("F"));
                    tts = tts.Replace(tts, ustts.ToString("F"));
                    deal = deal.Replace(deal, usDeal.ToString("F"));
                }


                currencyList.Add(new Currency(unit, countryName, ttb, tts, deal));
            }


            CurrencyLv.Items.Refresh();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currencyList.Clear();
            LblConvert.Content = "";
            string total = "";

            if (CurrDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a due date");
                return;

            }

            string strURL = "https://www.koreaexim.go.kr/site/program/financial/exchangeJSON?authkey="
                + "UKWgdmFHL143H1oAPWs9IuGM2y25uiA2&searchdate=" +
               CurrDatePicker.SelectedDate.Value.ToString("yyyyMMdd") + "&data=AP01";


            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(strURL);
            hwr.ContentType = "application/json";

            using (HttpWebResponse hwrResult = hwr.GetResponse() as HttpWebResponse)
            {
                Stream sr = hwrResult.GetResponseStream(); //Json Parsing
                using (StreamReader srd = new StreamReader(sr))
                {
                    string strResult = srd.ReadToEnd();
                    string strErr = "";
                    if (!StringToJson(strResult, ref strErr))
                    {
                        MessageBox.Show("It is not a business day and inquiry is not possible.");
                    }
                }
                sr.Close();
                hwrResult.Close();

            }

            // ComboBox combo = (ComboBox)sender;
            int amount = int.Parse(TxtAmount.Text);

            string currencyFrom = "";
            string currencyTo = "";


            currencyTo = ComboboxTo.SelectedItem.ToString();
            currencyFrom = ComboboxFrom.SelectedItem.ToString();

            if (currencyFrom == "CANADA - CAD" && currencyTo == "US     - USD")
            {
                total = (amount * (usDeal / cadDeal)).ToString("F");
                LblConvert.Content = total;
                total = "";
            }


            if (currencyFrom == "CANADA - CAD" && currencyTo == "KOREA  - KRW")
            {
                total = (amount * (cadDeal / krwDeal)).ToString("F");
                LblConvert.Content = total;
                total = "";
            }


            if (currencyFrom == "KOREA  - KRW" && currencyTo == "CANADA - CAD")
            {
                total = (amount * (cadDeal / (krwDeal * 1000000))).ToString("F");
                LblConvert.Content = total;
                total = "";
            }

            if (currencyFrom == "US     - USD" && currencyTo == "CANADA - CAD")
            {
                total = (amount * ((cadDeal / 1000) / usDeal)).ToString("F");
                LblConvert.Content = total;
                total = "";
            }
            if (currencyFrom == "US     - USD" && currencyTo == "KOREA  - KRW")
            {
                total = (amount * (krwDeal / usDeal)).ToString("F");
                LblConvert.Content = total;
                total = "";

            }
            if (currencyFrom == "KOREA  - KRW" && currencyTo == "US     - USD")
            {

                total = (amount * (usDeal / (krwDeal * 1000))).ToString("F");
                LblConvert.Content = total;
                total = "";
            }


        }

        private void ComboboxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void ComboboxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
