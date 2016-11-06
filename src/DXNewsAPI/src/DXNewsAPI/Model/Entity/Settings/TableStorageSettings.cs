namespace DXNewsAPI.Model.Entity.Settings
{
    public class TableStorageSettings
    {
        public string ConnectionString { get; set; }
        public string NewsTableId { get; set; }
        public string NewsPartition { get; set; }
    }
}
