using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXNewsAPI.Model.Entity
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
    }
}
