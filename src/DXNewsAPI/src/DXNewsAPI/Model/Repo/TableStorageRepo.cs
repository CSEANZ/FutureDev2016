using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NuGet.Packaging;

namespace DXNewsAPI.Model.Repo
{
    public class TableStorageRepo : ITableStorageRepo
    {
        private readonly IOptions<TableStorageSettings> _dbOptions;
        private readonly IMapper _mapper;

        private CloudStorageAccount _account;
        private CloudTable _table;

        public TableStorageRepo(IOptions<TableStorageSettings> dbOptions, IMapper mapper)
        {
            _dbOptions = dbOptions;
            _mapper = mapper;

            _account = CloudStorageAccount.Parse(dbOptions.Value.ConnectionString);
            var tableClient = _account.CreateCloudTableClient();
            
            _table = tableClient.GetTableReference(dbOptions.Value.NewsTableId);
        }

        public async Task Init()
        {
            await _table.CreateIfNotExistsAsync();
        }

        public async Task<bool> InsertNewsItem(NewsItem item)
        {
            var te = _mapper.Map<NewsItemTableEntity>(item);

            te.PartitionKey = _dbOptions.Value.NewsPartition;
            te.RowKey = string.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks) + Guid.NewGuid();

            var insertOp = TableOperation.Insert(te);

            var result = await _table.ExecuteAsync(insertOp);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        public async Task<bool> UpdateNewsItem(NewsItem item)
        {
            var existing = await _getNewsItemById(item.Id);

            var te = _mapper.Map<NewsItemTableEntity>(item);

            te.PartitionKey = existing.PartitionKey;
            te.ETag = existing.ETag;

            var insertOp = TableOperation.Replace(te);

            var result = await _table.ExecuteAsync(insertOp);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        public async Task<NewsItem> GetNewsItemById(string id)
        {
            var newsTableItem = await _getNewsItemById(id);

            return _mapper.Map<NewsItem>(newsTableItem);
        }

        async Task<NewsItemTableEntity> _getNewsItemById(string id)
        {
            var retrieveOperation = TableOperation.Retrieve<NewsItemTableEntity>(_dbOptions.Value.NewsPartition, id);

            var result = await _table.ExecuteAsync(retrieveOperation);

            var newsTableItem = result?.Result as NewsItemTableEntity;

            return newsTableItem;
        }

        public async Task<IList<NewsItem>> GetNewsItems(int take = 100)
        {
            string rowKeyToUse = string.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks);

            var query = new TableQuery<NewsItemTableEntity>().
                Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                        _dbOptions.Value.NewsPartition));

            query = query.Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual,
                rowKeyToUse));

            query = query.Take(take);

            TableContinuationToken token = null;

            var newsList = new List<NewsItemTableEntity>();
            do
            {
                var resultSegment = await _table.ExecuteQuerySegmentedAsync(query, token);

                token = resultSegment.ContinuationToken;

                newsList.AddRange(resultSegment.Results);

            } while (token != null);

            return _mapper.Map<List<NewsItem>>(newsList);
        }

        public async Task<bool> DeleteNewsItem(string id)
        {
            var item = await _getNewsItemById(id);

            if (item == null)
            {
                return false;
            }

            TableOperation deleteOperation = TableOperation.Delete(item);

            // Execute the operation.
            var result = await _table.ExecuteAsync(deleteOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }
    }
}
