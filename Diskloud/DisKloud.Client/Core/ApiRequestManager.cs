using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace DisKloud.Client.Core
{
    internal class ApiRequestManager
    {
        private string Apiurl;
        private string apikey;
        public ApiRequestManager(string server) 
        {
            Apiurl = $"http://{server}/api/";
        }
        
        public async Task<bool> GetApiKey (string user, string password)
        {
            HttpResponseMessage rep = await SendRequest(HttpMethod.Get, $"{Apiurl}Users/?username={user}&password={password}");

            if (rep.StatusCode == System.Net.HttpStatusCode.OK)
            {
                apikey = await rep.Content.ReadAsStringAsync();
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
