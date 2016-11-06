using System.Threading.Tasks;

namespace DXNewsAPI.Model.Contract
{
    public interface INotificationService
    {
        /// <summary>
        /// Sends a message to a Service Bus queue that can be picked up by what ever
        /// Cool thing is we don't know what other engineers on our system will do with this
        /// In our case, we pick it up and send it on to a notification hub and twitter. 
        /// </summary>
        /// <returns></returns>
        Task NotifySubscribers(string content);
    }
}