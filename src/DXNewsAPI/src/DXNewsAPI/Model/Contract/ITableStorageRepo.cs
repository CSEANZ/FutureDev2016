using System.Collections.Generic;
using System.Threading.Tasks;
using DXNewsAPI.Model.Entity;

namespace DXNewsAPI.Model.Contract
{
    public interface ITableStorageRepo
    {
        Task Init();
        Task<bool> InsertNewsItem(NewsItem item);
        Task<IList<NewsItem>> GetNewsItems();
        Task<NewsItem> GetNewsItemById(string id);
        Task<bool> UpdateNewsItem(NewsItem item);
        Task<bool> DeleteNewsItem(string id);
    }
}