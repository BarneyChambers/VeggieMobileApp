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
        //Network network = NBitcoin.Altcoins.Veggiecoin.Instance.Mainnet;
        Network brazNetwork = NBitcoin.Altcoins.Brazio.Instance.Mainnet;
        Key key;
        Key brazKey;

        GetInfoObj getInfoObj;
        public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            GetInfo();
            //Console.WriteLine(key.PubKey.GetAddress(network));
            //Console.WriteLine(key.GetBitcoinSecret(network));
            //Console.WriteLine("BITCOIN SECRET: " + key.GetWif(network).ToString() + "\n");
            brazKey = new Key();
            string brazAddress = brazKey.PubKey.GetAddress(brazNetwork).ToString();

            key = GetKeyFromWif();
            string address = GetAddressFromFile();
                        
            if (transactionList.ItemsSource == null)
            {
                List <transactionItem> l = new List<transactionItem>();

                l.Add(new transactionItem {
                    imageSource = "paperplaneFA.png",
                    status = "Sent",
                    amount = "-502.2357"+" VEGI",
                    timeOfTransaction = "a few seconds ago"
                });
                transactionList.ItemsSource = l;
                transactionList.IsEnabled = false;
            }
        }

        async void GetInfo()
        {
            try
            {
                APICommunicator a = new APICommunicator();
                string s = await a.BlockchainGetInfo();
                GetInfoObj g = getInfoDeserialized(s);
            }
            catch (Exception e)
            {
                await DisplayAlert("Oops!", "Failed to connect, please try again later.", "Ok");
            }

        }
        GetInfoObj getInfoDeserialized(string s)
        {
            return JsonConvert.DeserializeObject<GetInfoObj>(s);
        }

        //Gets the WIF from the user's mobile phone at wif.txt. If it doesn't exist, make a new wif.
        Key GetKeyFromWif()
        {
            string filename = Path.Combine(path, "brazwif.txt");
            string savedWif = "-1";
            try
            {
                using (var streamReader = new StreamReader(filename))
                {
                    savedWif = streamReader.ReadToEnd();
                    Key key = Key.Parse(savedWif.Substring(0, savedWif.Length - 1), brazNetwork);

                    return key;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: " + ex);
                Key key = new Key();
                using (var streamWriter = new StreamWriter(filename, true))
                {
                    string newWif = key.GetWif(brazNetwork).ToString();
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

        string GetAddressFromFile()
        {
            string filename = Path.Combine(path, "brazaddress.txt");
            string address ="-1";

            try
            {
                using (var streamReader = new StreamReader(filename))
                {
                    address = streamReader.ReadToEnd();
                    address = address.Substring(0, address.Length - 1);

                    return address;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: " + ex);
                Key key = GetKeyFromWif();
                
                using (var streamWriter = new StreamWriter(filename, true))
                {
                    address = key.PubKey.GetAddress(brazNetwork).ToString();
                    streamWriter.WriteLine(address);
                    return address;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error: " + ex);
            }

            return address;
        }
    }
}


