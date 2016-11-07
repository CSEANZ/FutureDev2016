using DXNewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DXNewsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.ViewModel;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // suppress the highlight on the cell
            newsListView.SelectedItem = null;

            // get the news item that was tapped on
            var newsItem = (NewsItem)e.Item;

            // navigate to the details page, passing the new item
            this.Navigation.PushAsync(new DetailPage(newsItem));
        }
    }
}
