using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Manga_Notifier.Scanlators;

namespace Manga_Notifier.Crawling
{
    public class Crawler
    {
        /// <summary>
        /// Starts the crawling process
        /// </summary>
        /// <param name="scanlator"></param>
        public void Crawl(IScanlators scanlator)
        {
            var client = ClientGenerator();
            try
            {
                var responsbody = CrawlPage(client, scanlator.Url).Result;
                if (string.IsNullOrWhiteSpace(responsbody))
                {
                    return;
                }
                var URLs = scanlator.GetAllComics(responsbody);

                foreach (var url in URLs)
                {
                    try
                    {
                        var responsBody = CrawlPage(client, url).Result;
                        if(!string.IsNullOrWhiteSpace(responsBody))
                        {
                            scanlator.ParseURLS(responsBody);
                        }
                    } 
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        Console.WriteLine(ex.Source);
                        Console.WriteLine(ex.Data);
                        Console.WriteLine(url);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Data);
                Console.WriteLine(scanlator.Url);
            }
        }

        /// <summary>
        /// Returns the source view from a page
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> CrawlPage(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            while(!response.IsSuccessStatusCode)
            {
                Thread.Sleep(2000);
                response = await client.GetAsync(url);
                Thread.Sleep(2000);
            }
            return await client.GetStringAsync(url);
        }

        /// <summary>
        /// Creates the client for the crawler (can be moved to seperate class if needed)
        /// </summary>
        /// <returns></returns>
        private HttpClient ClientGenerator()
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.");
            return client;
        }
    }
}
