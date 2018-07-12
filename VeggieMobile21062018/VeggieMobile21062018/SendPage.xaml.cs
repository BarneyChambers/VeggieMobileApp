using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace VeggieMobile21062018
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        public SendPage(ZXing.Result result)
        {
            InitializeComponent();
            if (result!=null)
            {
                checkResult(result.ToString());
            }
        }

        string checkResult(string resultString) { return "-1" ; }

        void TrimAmountEntry()
        {
            if (!amountEntry.Text.Contains("V"))
            {
                amountEntry.Text = "V" + amountEntry.Text;
            }
        }

        async void doScan()
        {
            try
            {
                /*
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtBarcode.Text = result;
                }*/

                var scanPage = new ZXingScannerPage();
                // Navigate to our scanner page
                await Navigation.PushAsync(scanPage);

                scanPage.OnScanResult += (result) =>
                {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void sendByAddress() { }
    }
}