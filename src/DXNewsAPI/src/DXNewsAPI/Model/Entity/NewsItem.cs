using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DXNewsAPI.Model.Entity
{
    public class NewsItem
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
