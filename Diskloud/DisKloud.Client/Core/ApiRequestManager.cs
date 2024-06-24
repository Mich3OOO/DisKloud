using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Net.Http.Json;

namespace DisKloud.Client.Core
{
    internal class ApiRequestManager
    {
        private string Apiurl;
        private string apikey;
        private string UserId;
        public ApiRequestManager(string server) 
        {
            Apiurl = $"http://{server}/api/";
        }
        
        public async Task<bool> GetApiKey (string user, string password)
        {
            Dictionary<string,string> rep = await (await SendRequest(HttpMethod.Get, $"{Apiurl}Users/?username={user}&password={password}")).Content.ReadFromJsonAsync< Dictionary<string, string>>();

            if (rep is not null)
            {
                apikey = rep["key"];
                UserId = rep["id"];
                Console.WriteLine($"Received {apikey}");
                return true;
            }

            return false;

        }
        
        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(method, url);
            return await client.SendAsync(request);
        }

    }
}
