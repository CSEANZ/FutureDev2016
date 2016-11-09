using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechExample.Utils
{

    public class KeyManagement
    {
        private string _subscriptionKey;

        private const string IsolatedStorageSubscriptionKeyFileName = "Subscription.txt";
        private const string DefaultSubscriptionKeyPromptMessage = "";

       

        public string SubscriptionKey
        {
            get
            {
                return this._subscriptionKey;
            }

            set
            {
                this._subscriptionKey = value;
            }
        }

        public string GetSubscriptionKeyFromIsolatedStorage()
        {
            string subscriptionKey = null;

            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                try
                {
                    using (var iStream = new IsolatedStorageFileStream(IsolatedStorageSubscriptionKeyFileName, FileMode.Open, isoStore))
                    {
                        using (var reader = new StreamReader(iStream))
                        {
                            subscriptionKey = reader.ReadLine();
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    subscriptionKey = null;
                }
            }

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                subscriptionKey = DefaultSubscriptionKeyPromptMessage;
            }

            SubscriptionKey = subscriptionKey;

            return subscriptionKey;
        }

        public void SaveSubscriptionKeyToIsolatedStorage()
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                using (var oStream = new IsolatedStorageFileStream(IsolatedStorageSubscriptionKeyFileName, FileMode.Create, isoStore))
                {
                    using (var writer = new StreamWriter(oStream))
                    {
                        writer.WriteLine(_subscriptionKey);
                    }
                }
            }
        }
    }
}
