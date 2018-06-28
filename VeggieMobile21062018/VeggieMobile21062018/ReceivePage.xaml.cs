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
        }

        private async void btnReceive_Clicked(object sender, EventArgs e)
        {
            QRCodeView.IsVisible = true;

            QRCodeView = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 150,
                    Width = 150,
                    PureBarcode = true
                },
                BarcodeValue = SetQrContent(),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            
        }

        string SetQrContent()
        {
            string QrContent="Bitcoin:"+address;
            if (amountTxt.Text != "")
            {
                QrContent += "?amount=" + addSigFigs(amountTxt.Text);
                
            }
            if (txtBarcode.Text != "")
            {
                QrContent += "&label=" + txtBarcode.Text;
            }
            if (msgText.Text != "")
            {
                QrContent += "&message=" + msgText.Text;
            }
            Console.WriteLine(QrContent);
            return QrContent;
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

