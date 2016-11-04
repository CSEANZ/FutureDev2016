using DXNewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DXNewsApp
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(NewsItem item)
        {
            InitializeComponent();
            BindingContext = item;
        }
    }
}
