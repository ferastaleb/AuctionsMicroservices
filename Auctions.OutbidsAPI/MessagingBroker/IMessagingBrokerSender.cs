using System;
using System.Threading.Tasks;

namespace Auctions.OutbidsAPI.MessagingBroker
{
    public interface IMessagingBrokerSender<T>
    {
        Task SendAsync(T message);
    }
}
