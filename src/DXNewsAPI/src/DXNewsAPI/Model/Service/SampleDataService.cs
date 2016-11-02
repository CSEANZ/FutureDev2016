using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;

namespace DXNewsAPI.Model.Service
{
    public class SampleDataService : IDataService
    {
#pragma warning disable 1998 //Allow because other real impls will use async
        
        /*On the theme of 1998
         * The top three songs on the ARIA chart in 1998 were
         * 
         *  - THE CUP OF LIFE / MARIA - Ricky Martin
         *  - IT'S LIKE THAT - Run DMC Vs Jason Nevins
         *  - IRIS - Goo Goo Dolls
         */

        public async Task<List<NewsItem>> SampleDataNews()
#pragma warning restore 1998
        {
            return new List<NewsItem>
            {
                new NewsItem
                {
                    Title = "Man teaches peeps things",
                    Abstract = "Teach a man to code and he will make an app",
                    Body = "Jordo was ere and there was news",
                    ImageUrl = "https://university.xamarin.com/images/team/kym-phillpotts.jpg"
                },
                new NewsItem
                {
                    Title = "Surface Studio is extra nice",
                    Abstract = "It's a nice device - now to get one somehow!",
                    Body = "In other news, Microsoft staff all want Surface Studio plz. TYVM.",
                    ImageUrl = "http://www.plaffo.com/wp/wp-content/uploads/2016/10/Surface-Studio.jpg"
                }

            };
        }
    }
}
