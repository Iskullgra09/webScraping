using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace webScrapingMultiprocessing
{
    public class DataService
    {
        public async Task SendJsonAsync(string json)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    "http://localhost:5000/json",
                    new StringContent(json, Encoding.UTF8, "application/json")).Result;
            }
        }
        
    }
}