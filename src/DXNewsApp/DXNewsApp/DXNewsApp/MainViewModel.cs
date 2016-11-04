using DXNewsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXNewsApp
{
    public class MainViewModel
    {
        IDXNewsAppClient client;

        public IList<NewsItem> NewsItems { get; private set; }

        public MainViewModel(IDXNewsAppClient client)
        {
            this.client = client;
            NewsItems = new ObservableCollection<NewsItem>(client.GetNews());
        }

        // these properties are really just here for designer data
        public string Body { get { return NewsItems.First().Body; } }
        public string ImageUrl { get { return NewsItems.First().ImageUrl; } }
        public string Title { get { return NewsItems.First().Title; } }

    }
}
