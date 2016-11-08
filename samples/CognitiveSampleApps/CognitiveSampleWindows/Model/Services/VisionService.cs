using System;
using System.Threading.Tasks;
using CognitiveSampleWindows.Model.Entity;
using ExtensionGoo.Standard.Extensions;

namespace CognitiveSampleWindows.Model.Services
{
    public class VisionService
    {
        private readonly SettingsService _settings;

        public VisionService(SettingsService settings)
        {
            _settings = settings;
        }

        public async Task<VisionResponse> DetectImage(byte[] image)
        {
            var dtStart = DateTime.Now;

            var url = _settings.GetCognitiveServicesUrl();

            var result = await url.PostAndParse<VisionResponse>(image);

            var dtEnd = DateTime.Now;

            var totalTime = dtEnd.Subtract(dtStart).TotalMilliseconds;

            return result;
        }
    }
}
