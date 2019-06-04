using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctions.OutbidsAPI.MessagingBroker;
using Auctions.OutbidsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Auctions.OutbidsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutbidController : ControllerBase
    {
        private readonly IMessagingBrokerSender<Message> _messagingBrokerSender;

        public OutbidController(IMessagingBrokerSender<Message> messagingBrokerSender)
        {
            _messagingBrokerSender = messagingBrokerSender;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Outbid outBidModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(outBidModel)))
                    {
                        MessageId = $"{outBidModel.BidId}-{outBidModel.Amount}",
                        SessionId = outBidModel.BidId.ToString()
                    };
                    await _messagingBrokerSender.SendAsync(message);
                    return Ok();
                }
                else
                    return StatusCode(400, ModelState);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
