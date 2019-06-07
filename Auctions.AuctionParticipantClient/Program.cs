using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Auctions.AuctionParticipantClient
{
    class Program
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = "auctions.redis.cache.windows.net:6380,password=R4rtOUtH+o6FShmsCp9NID6Qfx9YMtZU5di8mJm6HSs=,ssl=True,abortConnect=False";
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static readonly HttpClient client = new HttpClient();
        private static Guid bidId = new Guid("005AAAA1-1B6A-4A2B-A6F7-3FD5C85CD43E");
        static async Task DisplayCacheInfoAsync()
        {

            var bidsListKey = $"bids{bidId}";
            var topOutbidKey = $"TopOutbid{bidId}";

            IDatabase cache = lazyConnection.Value.GetDatabase();
            var topOutbid = await cache.StringGetAsync(topOutbidKey);
            var listLength = await cache.ListLengthAsync(bidsListKey);
            Console.WriteLine($"---------------------------");
            Console.WriteLine($"Bid Info In Cache BidId: {bidId}");
            Console.WriteLine($"Bid Info In Cache topAmount : {topOutbid}");
            Console.WriteLine($"Bid Info In Cache List Length : {listLength}");
            Console.WriteLine($"---------------------------");


            //var file = File.CreateText(@"C:\Users\FerasTaleb\Documents\GitHub\AuctionsMicroservices\Auctions.AuctionParticipantClient\list.txt");
            //long end = 1000;
            //long start = 0;
            //while (end < listLength)
            //{
            //    var list = await cache.ListRangeAsync(bidsListKey, start, end);
            //    foreach (var item in list)
            //    {
            //        await file.WriteLineAsync(item.ToString());
            //    }

            //    if (listLength > end)
            //    {
            //        start = end + 1;
            //        end += end;
            //    }
            //    else
            //    {
            //        start = end + 1;
            //        end = listLength;
            //    }
            //}
            //await file.FlushAsync();
            //file.Close();
        }
        static async Task Main(string[] args)
        {

            //            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
            Console.WriteLine("Hello AuctionsMicroservices!");


            var _continue = true;


            //var list = await cache.ListRangeAsync(bidsListKey, 0, -1);

            //Console.WriteLine("Enter BidId ");
            //bidId = Convert.ToInt32(Console.ReadLine());

            while (_continue)
            {
                Console.WriteLine("Enter c to display cache info");
                var c = Convert.ToString(Console.ReadLine());
                if (c == "c")
                {
                    await DisplayCacheInfoAsync();
                }


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
                                                          new StringContent(JsonConvert.SerializeObject(outbid), Encoding.UTF8, "application/json"));

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
