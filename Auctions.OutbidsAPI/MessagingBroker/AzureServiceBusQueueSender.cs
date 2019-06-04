using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auctions.OutbidsAPI.MessagingBroker
{
    public class AzureServiceBusQueue : IMessagingBrokerSender<Message>
    {
        private readonly MessageSender sender;

        public AzureServiceBusQueue(string connectionString, string entityPath)
        {
            sender = new MessageSender(connectionString, entityPath, RetryPolicy.Default);
        }

        public async Task SendAsync(Message message)
        {
            await sender.SendAsync(message);
            await sender.CloseAsync();
        }
    }
}