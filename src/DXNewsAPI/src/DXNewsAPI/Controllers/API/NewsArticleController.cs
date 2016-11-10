using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using DXNewsAPI.Model.Entity.News;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DXNewsAPI.Controllers.API
{
    [Route("api/[controller]")]
    public class NewsArticleController : Controller
    {
        private readonly ITableStorageRepo _tableStorageRepo;
        

        public NewsArticleController(ITableStorageRepo tableStorageRepo)
        {
            _tableStorageRepo = tableStorageRepo;
        }

        [SwaggerOperation("GetNews")]
        [ProducesResponseType(typeof(IEnumerable<NewsItem>), 200)]
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(await _tableStorageRepo.GetNewsItems());
        }

        [SwaggerOperation("SearchNews")]
        [ProducesResponseType(typeof(IEnumerable<NewsItem>), 200)]
        [HttpGet]
        [Route("Search/{searchString}")]

        public async Task<IActionResult> Search(string searchString)
        {
            return Ok(await _tableStorageRepo.GetNewsItems(searchString));
        }

        [SwaggerOperation("LatestNewsItem")]
        [ProducesResponseType(typeof(NewsItem), 200)]
        [HttpGet]
        [Route("GetLatest")]
        public async Task<IActionResult> GetLatest()
        {
            var latest = await _tableStorageRepo.GetNewsItems(null, 1);
            var item = latest?.FirstOrDefault();
            return Ok(item);
        }

    }
}
