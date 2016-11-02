using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DXNews.SDK;

namespace IntegrationTests
{
    public class TestBase
    {
        public IDXNewsAPI Client { get; }

        public TestBase()
        {
            var client = new DXNewsAPI();
            client.BaseUri = new Uri("http://dxnews.azurewebsites.net/");
            Client = client;
        }

    }
}
