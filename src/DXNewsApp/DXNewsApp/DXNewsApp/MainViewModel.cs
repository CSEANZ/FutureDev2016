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

        public string Message { get; set; } = "Hello";

        public MainViewModel(IDXNewsAppClient client)
        {
            this.client = client;
            NewsItems = new ObservableCollection<NewsItem>(client.GetNews());
        }
    }
}
