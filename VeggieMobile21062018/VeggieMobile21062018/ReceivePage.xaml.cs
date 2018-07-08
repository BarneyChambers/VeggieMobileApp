using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using VeggieMobile21062018;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace VeggieMobile21062018
{
    public partial class ReceivePage : ContentPage
    {
        private string codeValue= "-1";
        public string address;
        public ReceivePage(string receiveAddress)
        {
            InitializeComponent();
            address = receiveAddress;
            NavigationPage.SetHasNavigationBar(this, false);

        }
        async Task<string> SetQrContent()
        {
            string QrContent = "Bitcoin:" + address;
            if (amountTxt.Text != null)
            {
                QrContent += "?amount=" + addSigFigs(amountTxt.Text);

            }
            if (txtBarcode.Text != null)
            {
                QrContent += "&label=" + txtBarcode.Text;
            }
            if (msgText.Text != null)
            {
                QrContent += "&message=" + msgText.Text;
            }
            Console.WriteLine(QrContent);
            return QrContent;
        }

        async void MyEntryChanged(TextChangedEventArgs e)
        {
            //string bv = await SetQrContent();


                //QRCodeView = null;
                QRCodeView = new ZXingBarcodeImageView
                {
                    BarcodeFormat = BarcodeFormat.QR_CODE,
                    BarcodeOptions = new QrCodeEncodingOptions
                    {
                        Height = 150,
                        Width = 150,
                        PureBarcode = true
                    },
                    

                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                QRCodeView.IsVisible = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                QRCodeView.BarcodeValue = await SetQrContent();
            });
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

