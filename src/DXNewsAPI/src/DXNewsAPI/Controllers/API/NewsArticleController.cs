using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DXNewsAPI.Controllers.API
{
    [Route("api/[controller]")]
    public class NewsArticleController : Controller
    {
        private readonly IDataService _sampleDataService;

        public NewsArticleController(IDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        [SwaggerOperation("GetNews")]
        [ProducesResponseType(typeof(IEnumerable<NewsItem>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sampleDataService.SampleDataNews());
        }

    }
}
