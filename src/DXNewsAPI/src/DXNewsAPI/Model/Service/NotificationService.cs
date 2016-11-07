using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity.Settings;
using ExtensionGoo.Standard.Extensions;
using Microsoft.Extensions.Options;

namespace DXNewsAPI.Model.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IOptions<NotificationSettings> _notificationSettings;

        private string _callbackTemplate;
        public NotificationService(IOptions<NotificationSettings> notifictionOptions)
        {
            _notificationSettings = notifictionOptions;

            _callbackTemplate = notifictionOptions.Value.UrlCallback;
        }
        /// <summary>
        /// Sends a message to a Service Bus queue that can be picked up by what ever
        /// Cool thing is we don't know what other engineers on our system will do with this
        /// In our case, we pick it up and send it on to a notification hub and twitter. 
        /// </summary>
        /// <returns></returns>
        public async Task NotifySubscribers(string content)
        {
            var completedUrl = string.Format(_callbackTemplate, UrlEncoder.Default.Encode(content));
            await completedUrl.GetRaw();
        }
    }
}
