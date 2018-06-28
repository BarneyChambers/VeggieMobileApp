using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VeggieMobile21062018
{
	public partial class MainPage : ContentPage
	{
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        Network network = NBitcoin.Altcoins.Veggiecoin.Instance.Mainnet;
        Key key;
        public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //string address = key.PubKey.GetAddress(network).ToString();
            //Console.WriteLine(key.PubKey.GetAddress(network));
            //Console.WriteLine(key.GetBitcoinSecret(network));
            //Console.WriteLine("BITCOIN SECRET: " + key.GetWif(network).ToString() + "\n");
            key = GetKeyFromWif();
            
            if (transactionList.ItemsSource == null)
            {
                transactionList.ItemsSource = new List<string> { "Sent: 0.65900000 PET4MehsrcbsjXU5zGZkHWyjNzw7xxnHAt", "Received: 5.99340000 PS4YhHyUMEkUyiSbSoLQyvaGapWcsaK1Au", "Sent: 32.594485711 PSYjCLMHMoiHaPihNBGCcz3CG5zVUHF875", "Sent: 0.00000001 PMesDYjayZvA4ex3aTwNa61fZ3zJeUt3By", ""};
                transactionList.IsEnabled = false;
            }
        }


        //Gets the WIF from the user's mobile phone at wif.txt. If it doesn't exist, make a new wif.
        Key GetKeyFromWif()
        {
            string filename = Path.Combine(path, "wif.txt");
            string savedWif = "-1";
            try
            {
                using (var streamReader = new StreamReader(filename))
                {
                    savedWif = streamReader.ReadToEnd();
                    Key key = Key.Parse(savedWif.Substring(0, savedWif.Length - 1), network);

                    return key;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: " + ex);
                Key key = new Key();
                using (var streamWriter = new StreamWriter(filename, true))
                {
                    string newWif = key.GetWif(network).ToString();
                    streamWriter.WriteLine(newWif);
                    return key;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error: " + ex);
                return null;
            }
        }

        void ReceivePageClicked(EventArgs e)
        {
            string receiveAddress = key.PubKey.GetAddress(network).ToString();
            Navigation.PushAsync(new ReceivePage(receiveAddress));
        }
        void SendPageClicked(EventArgs e)
        {
            Navigation.PushAsync(new SendPage());
        }
    }
}


