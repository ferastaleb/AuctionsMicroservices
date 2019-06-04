using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auctions.MessagingBroker
{
    public interface IMessagingBrokerReciever<T>
    {
        Task<bool> RecieveMessageAsync();

        Task<bool> RecieveMessageOnSessionAsync();

        Task CloseAsync();

    }
}
