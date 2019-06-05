using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace OutbidsWorker
{
    public static class OutbidsWorker
    {
        [FunctionName("OutbidsWorker")]
        public static async Task Run([ServiceBusTrigger("outbids", Connection = "Outbids_Queue")]Message myQueueItem, ILogger log)
        {
            var body = Encoding.UTF8.GetString(myQueueItem.Body);
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {body}");

            var outbid = JsonConvert.DeserializeObject<Outbid>(body);
            var cn = Environment.GetEnvironmentVariable("Outbids_SQLDbConnection");
            using (SqlConnection conn = new SqlConnection(cn))
            {
                await conn.OpenAsync();
                
                var sqlText = $"select Top 1 Id from outbids Where Amount >= {outbid.Amount} AND BidId ='{outbid.BidId}'";
                var isExists = false;
                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    isExists = await cmd.ExecuteScalarAsync() != null;
                    log.LogInformation($"isExists value : {isExists}- For BidId:{outbid.BidId}");
                }

                //if (isExists)
                //    return;
                outbid.Id = Guid.NewGuid();
                var insertText = "INSERT INTO OUTBIDS(Id,BidId,Amount) VALUES(@Id,@BidId,@Amount);";
                using(SqlCommand cmdInsert = new SqlCommand(insertText, conn))
                {
                    cmdInsert.Parameters.Add(new SqlParameter("@Id", Guid.NewGuid()));
                    cmdInsert.Parameters.Add(new SqlParameter("@BidId", outbid.BidId));
                    cmdInsert.Parameters.Add(new SqlParameter("@Amount", outbid.Amount));

                    var affectedRows = await cmdInsert.ExecuteNonQueryAsync();

                    log.LogInformation($"affectedRows value : {affectedRows}- For BidId:{outbid.BidId}");
                }
            }


        }
        public class Outbid
        {
            public Guid Id { get; set; }

            public Guid BidId { get; set; }

            public float Amount { get; set; }
        }
    }
}
