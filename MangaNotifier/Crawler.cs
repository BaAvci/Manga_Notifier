using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using static System.Net.WebRequestMethods;

namespace Manga_Notifier
{
    public class Crawler
    {
        private List<string> comicURLS = new();
        public async Task GetWebPage(string url)
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
            var responsBody = await client.GetStringAsync(url);
            if (!string.IsNullOrWhiteSpace(responsBody))
            {
                comicURLS = scanlators.GetAllComics(responsBody);
            }
            foreach (var comic in comicURLS)
            {
                responsBody = await client.GetStringAsync(comic);
                if (!string.IsNullOrWhiteSpace(responsBody))
                {
                    scanlators.ParseURLS(responsBody);
                }
            }
        }
    }
}
