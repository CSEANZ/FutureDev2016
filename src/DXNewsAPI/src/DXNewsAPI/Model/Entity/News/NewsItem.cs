using System.ComponentModel.DataAnnotations;

namespace DXNewsAPI.Model.Entity.News
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
