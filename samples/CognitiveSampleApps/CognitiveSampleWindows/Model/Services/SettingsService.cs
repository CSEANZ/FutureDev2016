using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveSampleWindows.Model.Services
{
    public class SettingsService
    {
        readonly Windows.Storage.ApplicationDataContainer _localSettings =
            Windows.Storage.ApplicationData.Current.LocalSettings;

        public string GetCognitiveServicesUrl()
        {
            var key = GetKey();

            var url =
                string.Format(
                    "https://api.projectoxford.ai/vision/v1.0/analyze?visualFeatures=Description,Tags&subscription-key={0}",
                    key);

            return url;
        }

        public void SetKey(string key)
        {
            _localSettings.Values["VisionKey"] = key;
        }

        public string GetKey()
        {
            if (_localSettings.Values.ContainsKey("VisionKey"))
            {
                return _localSettings.Values["VisionKey"]?.ToString();
            }

            return "";
        }
    }
}
