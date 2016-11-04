using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DXNewsAPI.Model.Entity
{
    /// <summary>
    /// Table entity for writing to the Azure Storage table. 
    /// We don't want to alter the original DTO with a base class like this as it will cause a smell in our app/Swagger
    /// Leave POCO Alone!!
    /// </summary>
    public class NewsItemTableEntity : TableEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
