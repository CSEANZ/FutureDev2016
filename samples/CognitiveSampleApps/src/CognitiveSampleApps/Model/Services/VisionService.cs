using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CognitiveSampleApps.Model.Entity;
using ExtensionGoo.Standard.Extensions;
using Microsoft.Extensions.Configuration;

namespace CognitiveSampleApps.Model.Services
{
    public class VisionService
    {
       

        private string _key;
        private string _endpoint;

        public VisionService(IConfiguration config, string extraConfig)
        {
            _key = config["CognitiveServicesKey"];
            _endpoint = config["CognitiveVisionEndpoint" + extraConfig];
        }

        public VisionResponse DetectImage(byte[] image)
        {
            var dtStart = DateTime.Now;
            
            Console.WriteLine($"Uploading to {_endpoint}");

            var url = string.Format(_endpoint, _key);

            var result = url.PostAndParse<VisionResponse>(image).GetAwaiter().GetResult();

            var dtEnd = DateTime.Now;

            var totalTime = dtEnd.Subtract(dtStart).TotalMilliseconds;

            Console.WriteLine($"Received result in {totalTime}ms");

            return result;
        }
    }
}
