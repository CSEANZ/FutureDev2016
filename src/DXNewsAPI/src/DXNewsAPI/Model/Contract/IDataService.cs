using System.Collections.Generic;
using System.Threading.Tasks;
using DXNewsAPI.Model.Entity;
using DXNewsAPI.Model.Entity.News;

namespace DXNewsAPI.Model.Contract
{
    public interface IDataService
    {
        Task<List<NewsItem>> SampleDataNews();
    }
}