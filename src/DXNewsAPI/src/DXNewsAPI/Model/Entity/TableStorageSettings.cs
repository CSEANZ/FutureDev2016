using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXNewsAPI.Model.Entity
{
    public class TableStorageSettings
    {
        public string ConnectionString { get; set; }
        public string NewsTableId { get; set; }
        public string NewsPartition { get; set; }
    }
}
