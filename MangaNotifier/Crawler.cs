using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Manga_Notifier
{
    public class Crawler
    {
        private readonly string _url = "https://asuratoon.com/manga/2423162651-the-greatest-estate-developer/";

        public async Task GetWebPage()
        {
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(60);
            var responsBody = await client.GetStringAsync(_url);
            if (!string.IsNullOrWhiteSpace(responsBody))
            {
                ParseURLS(responsBody);
            }
        }

        public void ParseURLS(string webPage)
        {
            Series_Info series_Info = new();
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webPage);
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//li[1]/div/div/a[@href]");

            series_Info.Name = htmlDocument.DocumentNode.SelectSingleNode("//h1").InnerText.Trim();
            series_Info.Id = int.Parse(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='bookmark']").GetAttributeValue("data-id","-999"));
            series_Info.URL = node.GetAttributeValue("href", string.Empty);
            series_Info.ChapterName = node.SelectSingleNode("./span[@class='chapternum']").InnerText.Trim();

            Console.WriteLine(series_Info.Id);
        }
    }
}
