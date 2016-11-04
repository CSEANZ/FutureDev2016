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

            te.PartitionKey = "NewsMain";
            te.RowKey = Guid.NewGuid().ToString();

            var insertOp = TableOperation.Insert(te);

            var result = await _table.ExecuteAsync(insertOp);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }
    }
}
