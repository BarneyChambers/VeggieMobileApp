using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VeggieMobile21062018
{
    public class APICommunicator
    {
        HttpClient client;
        const string serverIP = "http://202.182.102.196";
        const string getInfoPath = "getinfo";
        const string sendToAddressPath = "sendtoaddress";

        public async Task<string> BlockchainGetInfo()
        {
            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var serverUri = new Uri(serverIP);
                var relativeUri = new Uri(getInfoPath, UriKind.Relative);
                Uri fullUri = new Uri(serverUri, relativeUri);
                var response = await client.GetAsync(fullUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "-1";
        }

        public async Task<string> BlockchainSendToAddress(string address, string amount, string comment)
        {
            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var serverUri = new Uri(serverIP);
                var relativeUri = new Uri(sendToAddressPath, UriKind.Relative);
                var addressUri = new Uri(address, UriKind.Relative);
                var amountUri = new Uri(amount, UriKind.Relative);
                var commentUri = new Uri(comment, UriKind.Relative);
                Uri fullUri = new Uri(serverUri, relativeUri);

                if (!String.IsNullOrEmpty(address) && !String.IsNullOrEmpty(amount))
                {
                    fullUri = new Uri(fullUri, addressUri);
                    fullUri = new Uri(fullUri, amountUri);
                    Console.WriteLine(fullUri.ToString());
                }
                else
                {
                    return "You need to specify an address and an amount";
                }

                //TODO: add account?=XYZ
                var response = await client.GetAsync(fullUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "-1";
        }

    }
}
