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

        public CurrencyRate()
        {
            InitializeComponent();
            CurrencyLv.ItemsSource = currencyList;
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
                if (us)
                {
                        countryName =  countryName.Replace("미국 달러", "US");
                }
                else if (kor)
                {
                    countryName = countryName.Replace("한국 원", "KOREA");
                }
                else if (cad)
                {
                    countryName = countryName.Replace("캐나다 달러", "CANADA");
                }


                currencyList.Add(new Currency(unit, countryName, ttb, tts, deal));
            }


            CurrencyLv.Items.Refresh();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime currDatePicker = CurrDatePicker.SelectedDate.Value;
            string strURL = "https://www.koreaexim.go.kr/site/program/financial/exchangeJSON?authkey="
                + "UKWgdmFHL143H1oAPWs9IuGM2y25uiA2&searchdate=" +
               currDatePicker.ToString("yyyyMMdd") + "&data=AP01";


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
        }

    }
}
