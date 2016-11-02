using System.Threading.Tasks;
using DXNews.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Tests
{
    [TestClass]
    public class GetNews : TestBase
    {
        [TestMethod]
        public async Task GetAllNews()
        {
            var news = await Client.GetNewsAsync();

            Assert.IsNotNull(news);

            Assert.IsFalse(news.Count == 0);
        }
    }
}