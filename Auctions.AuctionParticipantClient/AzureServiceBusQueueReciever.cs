//using Microsoft.Azure.ServiceBus;
//using Microsoft.Azure.ServiceBus.Core;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Auctions.MessagingBroker
//{
//    public class AzureServiceBusQueueReciever : IMessagingBrokerReciever<Message>
//    {
//        private readonly QueueClient queueClient;
//        public AzureServiceBusQueueReciever(string connectionString, string entityPath)
//        {
//            queueClient = new QueueClient(connectionString, entityPath);
//        }

//        public async Task CloseAsync()
//        {
//            await queueClient.CloseAsync();
//        }

//        public async Task<bool> RecieveMessageAsync()
//        {
//            var doneReceiving = new TaskCompletionSource<bool>();
//            queueClient.RegisterMessageHandler(
//                async (message, cancellationToken1) =>
//                {

//                },
//                new MessageHandlerOptions((e) => LogMessageHandlerException(e)) { AutoComplete = false, MaxConcurrentCalls = 1 });

//            return await doneReceiving.Task;
//        }

//        public async Task<bool> RecieveMessageOnSessionAsync()
//        {
//            var doneReceiving = new TaskCompletionSource<bool>();
//            queueClient.RegisterSessionHandler(
//                async (imessage, message, cancellationToken1) =>
//                {

//                },
//                new SessionHandlerOptions((e) => LogMessageHandlerException(e))
//                {
//                    MessageWaitTimeout = TimeSpan.FromSeconds(5),
//                    MaxConcurrentSessions = 1,
//                    AutoComplete = false
//                });

//            return await doneReceiving.Task;
//        }


//        private Task LogMessageHandlerException(ExceptionReceivedEventArgs e)
//        {
//            Console.WriteLine("Exception: \"{0}\" {0}", e.Exception.Message, e.ExceptionReceivedContext.EntityPath);
//            return Task.CompletedTask;
//        }
//    }
//}
