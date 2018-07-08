using NBitcoin;
using NBitcoin.RPC;
using QBitNinja.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace VeggieMobile21062018
{
	public partial class MainPage : ContentPage
	{
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        Network network = NBitcoin.Altcoins.Veggiecoin.Instance.Mainnet;
        Key key;
        GetInfoObj getInfoObj;
        public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            GetInfo();
            //Console.WriteLine(key.PubKey.GetAddress(network));
            //Console.WriteLine(key.GetBitcoinSecret(network));
            //Console.WriteLine("BITCOIN SECRET: " + key.GetWif(network).ToString() + "\n");
            key = GetKeyFromWif();
            var address = key.PubKey.GetAddress(network);

            if (transactionList.ItemsSource == null)
            {
                transactionList.ItemsSource = new List<string> {"No transactions found.","","","",""};
                transactionList.IsEnabled = false;
            }
        }

        async void GetInfo()
        {
            APICommunicator a = new APICommunicator();
            string s = await a.BlockchainGetInfo();
            GetInfoObj g = getInfoDeserialized(s);
        }
        GetInfoObj getInfoDeserialized(string s)
        {
            return JsonConvert.DeserializeObject<GetInfoObj>(s);
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
        async void SettingPageClicked(EventArgs e)
        {
            APICommunicator a = new APICommunicator();
            string s = await a.BlockchainGetInfo();
            GetInfoObj g = getInfoDeserialized(s);

            await Navigation.PushAsync(new InfoPage(g));
        }
    }
}


