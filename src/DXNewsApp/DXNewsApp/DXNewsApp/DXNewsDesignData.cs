using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DXNewsApp.Models;
using Microsoft.Rest;
using System.Collections.ObjectModel;

namespace DXNewsApp
{
    public class DXNewsDesignData : IDXNewsAppClient
    {
        IList<NewsItem> mockItems = new ObservableCollection<NewsItem>();

        public DXNewsDesignData(){ }

        public DXNewsDesignData(Uri address) { }

        public Uri BaseUri { get; set; }

        public ServiceClientCredentials Credentials { get; set; }

        public void Dispose() { }

        string loremIpsumText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis dignissim ipsum, et commodo dolor aliquet eget. Aliquam erat volutpat. Donec in mi diam. Vivamus molestie vitae elit tincidunt aliquet. Quisque eu felis quis enim rutrum dictum in ac lectus. Etiam dictum turpis ut ullamcorper eleifend. Duis eu efficitur neque, eu porttitor sapien. Duis eros sem, pretium in dolor et, fringilla consequat est. Etiam vehicula nisl sed nisl rhoncus tempor. Ut vel dui sed arcu luctus blandit ac in odio. Donec condimentum erat in nisi cursus condimentum.\n\nMauris diam enim, ultricies et enim eget, condimentum porta dui. Nullam vehicula, mauris vel tincidunt euismod, magna purus feugiat nisl, molestie euismod orci ex id velit. Fusce nec diam eget leo pellentesque pellentesque. Donec nec consequat nulla, sed porttitor tellus. Duis vitae aliquet magna, condimentum euismod nibh. Fusce consequat gravida sem, vitae consectetur arcu tincidunt a. Cras vehicula nunc eget nisl dictum, ac consectetur lectus ullamcorper. Nam rutrum accumsan leo, vitae ultricies libero sagittis nec. Duis tempor semper eros. Etiam tellus nibh, vulputate sed aliquam vitae, condimentum vel ligula. Sed at magna odio. Quisque molestie mi non posuere cursus. Phasellus a neque nibh. Nunc et diam dapibus, ultricies odio ut, sodales erat.";

        public async Task<HttpOperationResponse<IList<NewsItem>>> GetNewsWithOperationResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            mockItems = new ObservableCollection<NewsItem>
            {
                new NewsItem
                {
                    Title = "New Surface Books",
                    Abstract = "The ultimate laptop. Now more powerful than ever.",
                    ImageUrl = "SurfaceBook.jpg",
                    Body = loremIpsumText
                },
                new NewsItem
                {
                    Title = "Surface Studio",
                    Abstract = "Turn your desk into a Studio.",
                    ImageUrl = "SurfaceStudio.jpg",
                    Body = loremIpsumText
                },
                new NewsItem
                {
                    Title = "Christmas with Xbox One",
                    Abstract = "Give the gift of gaming this Christmas",
                    ImageUrl = "XboxS.png",
                    Body = loremIpsumText
                },

            };
            HttpOperationResponse<IList<NewsItem>> response = new HttpOperationResponse<IList<NewsItem>>();
            response.Body = mockItems;
            return response;
        }

        public async Task<HttpOperationResponse<NewsItem>> LatestNewsItemWithOperationResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // get the latest news item
            var latestNews = mockItems.LastOrDefault();
            HttpOperationResponse<NewsItem> response = new HttpOperationResponse<NewsItem>();
            response.Body = latestNews;
            return response;

        }
    }
}
