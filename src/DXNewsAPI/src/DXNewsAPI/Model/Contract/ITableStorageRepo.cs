using System.Collections.Generic;
using System.Threading.Tasks;
using DXNewsAPI.Model.Entity;
using DXNewsAPI.Model.Entity.News;

namespace DXNewsAPI.Model.Contract
{
    public interface ITableStorageRepo
    {
        Task Init();
        Task<bool> InsertNewsItem(NewsItem item);
        Task<IList<NewsItem>> GetNewsItems(int take = 100);
        Task<NewsItem> GetNewsItemById(string id);
        Task<bool> UpdateNewsItem(NewsItem item);
        Task<bool> DeleteNewsItem(string id);
    }
}