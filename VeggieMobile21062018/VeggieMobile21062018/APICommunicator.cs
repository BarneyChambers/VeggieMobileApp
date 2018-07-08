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

        public async Task<string> BlockchainSendToAddress()
        {
            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var serverUri = new Uri(serverIP);
                var relativeUri = new Uri(sendToAddressPath, UriKind.Relative);
                Uri fullUri = new Uri(serverUri, relativeUri);
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
