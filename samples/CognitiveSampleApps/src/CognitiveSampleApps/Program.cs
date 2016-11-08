using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CognitiveSampleApps.Model.Services;
using Microsoft.Extensions.Configuration;

namespace CognitiveSampleApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("https://github.com/MSFTAuDX/FutureDev2016");

            if (args.Length == 0)
            {
                Console.WriteLine("Please pass in an image file location");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"Could not find the file {args[0]}");
                return;
            }

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

            var config = builder.Build();

            var file = File.ReadAllBytes(args[0]);

            string extraConfig = null;

            if (args.Length > 1)
            {
                extraConfig = $"_{args[1]}";
            }

            var visionService = new VisionService(config, extraConfig);

            var detectResult = visionService.DetectImage(file);

            if (detectResult == null)
            {
                Console.WriteLine("There was a problem!");
                return;
            }

            Console.WriteLine("************");
            foreach (var descriptionCaption in detectResult.description.captions)
            {
                Console.WriteLine(descriptionCaption.text);
            }

            var output = detectResult.tags.Select(detectResultTag => detectResultTag.name).ToList();

            Console.WriteLine($"Tags: {string.Join(", ", output)}");

            Console.ReadKey();

        }
    }
}
