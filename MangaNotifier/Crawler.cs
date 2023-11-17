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
        public async Task GetWebPage(string url, IScanlators scanlators)
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.");

            HttpResponseMessage response = await client.GetAsync(url);

           if (response.IsSuccessStatusCode)
           {
                var responsBody = await client.GetStringAsync(url);
                if (!string.IsNullOrWhiteSpace(responsBody))
                {
                    comicURLS = scanlators.GetAllComics(responsBody);
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
    }
}
