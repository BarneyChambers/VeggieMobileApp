using System;  
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using VeggieMobile21062018;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.Net.Mobile;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace VeggieMobile21062018
{
    public partial class ReceivePage : ContentPage, INotifyPropertyChanged
    {
        public string address;
        private Random _random = new Random();
        private string _barcodeValue = "VeggieCoin:WALLETADDRESS";

        public string BarcodeValue
        {
            get { return _barcodeValue; }
            set
            {

                _barcodeValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BarcodeValue)));
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        //public EncodingOptions b => new EncodingOptions() { Height = 300, Width = 300, PureBarcode = false, Margin = 10 };
        public ReceivePage(string receiveAddress)
        {
            BindingContext = this;
           

                InitializeComponent();
            address = receiveAddress;
            //NavigationPage.SetHasNavigationBar(this, false);
            

        }
        public static string GetWalletAddress()
        {
            /* 
             * 
             * TODO:
             * Fetch wallet address from file
             *
             */

            return "WALLETADDRESS";
            
        }

        void UpdateQRLabel()
        {
            string walletAddress = GetWalletAddress();
            string amount = VeggieEntry.Text;
            string label = Label.Text;
            string message = Message.Text;

            //no information
            if (string.IsNullOrWhiteSpace(amount) && string.IsNullOrWhiteSpace(label) && string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS";
                return;
            }
            //amount
            if (!string.IsNullOrWhiteSpace(amount) && string.IsNullOrWhiteSpace(label) && string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?amount=" + amount.Replace("V", "").Replace(" ", "");
                return;
            }
            //label
            if (string.IsNullOrWhiteSpace(amount) && !string.IsNullOrWhiteSpace(label) && string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?label=" + label;
                return;
            }
            //message
            if (string.IsNullOrWhiteSpace(amount) && string.IsNullOrWhiteSpace(label) && !string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?message=" + message;
                return;
            }
            //amount and label
            if (!string.IsNullOrWhiteSpace(amount) && !string.IsNullOrWhiteSpace(label) && string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?amount=" + amount.Replace("V", "").Replace(" ", "") + "&label=" + label;
                return;
            }
            //amount and message
            if (!string.IsNullOrWhiteSpace(amount) && string.IsNullOrWhiteSpace(label) && !string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?amount=" + amount.Replace("V", "").Replace(" ", "") + "&message=" + message;
                return;
            }
            //label and message
            if (string.IsNullOrWhiteSpace(amount) && !string.IsNullOrWhiteSpace(label) && !string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?label=" + label + "&message=" + message;
                return;
            }
            //label and amount and message
            if (!string.IsNullOrWhiteSpace(amount) && !string.IsNullOrWhiteSpace(label) && !string.IsNullOrWhiteSpace(message))
            {
                QRString.Text = "VeggieCoin:WALLETADDRESS" + "?amount=" + amount.Replace("V", "").Replace(" ","") + "?label=" + label + "&message=" + message;
                return;
            }

        }

        public void Handle_Clicked(object sender, EventArgs e)
        {
            UpdateQRLabel();
            BarcodeValue = QRString.Text;
        }

        async void VeggieEntryTextChanged(TextChangedEventArgs e)
        {
           
            if (!VeggieEntry.Text.Contains("V"))
            {
                VeggieEntry.Text = "V " + VeggieEntry.Text;
            }
            if (VeggieEntry.Text.Length > 0)
            {
                USDEntry.Text = "$" + VeggieEntry.Text.Substring(1, VeggieEntry.Text.Length - 1);
            }
            UpdateQRLabel();


        }

        void OnTapped(object sender, EventArgs args)
        {
            Console.WriteLine("Click Event Received...");
            VeggieEntry.Text = "";
            USDEntry.Text = "";

        }


        public static String addSigFigs(String str)
        {
            if (!str.Contains("."))
            {
                str = str + ".";
            }
            int pFrom = str.IndexOf(".") + ".".Length;
            int zeros = str.Length - pFrom;
            while (zeros < 8)
            {
                str += "0";
                zeros++;
            }
            return str;
        }
    }
}

