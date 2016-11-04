using System.Threading.Tasks;
using DXNewsAPI.Model.Entity;

namespace DXNewsAPI.Model.Contract
{
    public interface ITableStorageRepo
    {
        Task Init();
        Task<bool> InsertNewsItem(NewsItem item);
    }
}