using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Auctions.AuctionParticipantClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {

//            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
            Console.WriteLine("Hello AuctionsMicroservices!");


            var _continue = true;
            var bidId = Guid.NewGuid();

            //Console.WriteLine("Enter BidId ");
            //bidId = Convert.ToInt32(Console.ReadLine());

            while (_continue)
            {
                Console.WriteLine($"Enter your Outbid Amount For the Bid {bidId}");
                var amount = Convert.ToSingle(Console.ReadLine());
                var outbid = new
                {
                    BidId = bidId,
                    Amount = amount
                };
                try
                {
                    var response = await client.PostAsync("https://outbids-api.azurewebsites.net/api/outbid",
                                                        //"https://localhost:5001/api/outbid",
                                                          new StringContent(JsonConvert.SerializeObject(outbid), Encoding.UTF8,"application/json"));

                    Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"EXC: {exc.Message}");
                    throw;
                }

            }

        }
    }
}
