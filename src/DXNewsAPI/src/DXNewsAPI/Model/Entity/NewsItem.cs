using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;

namespace DXNewsAPI.Model.Entity
{
    public class NewsItem
    {
        public string Id { get; set; }
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
