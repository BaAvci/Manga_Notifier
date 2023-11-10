using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier
{
    public class FlamScans : IScanlators
    {
        private readonly string url;
        private List<Series_Info> seriesInfo;
        public FlamScans(string url)
        {
            this.url = url;
            seriesInfo = new List<Series_Info>();
        }

        public List<string> GetAllComics(string responsBody)
        {
            HtmlDocument htmlDocument = new();
            List<string> comicSeriesInfos = new();
            htmlDocument.LoadHtml(responsBody);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='bs']/div/a");
            foreach (HtmlNode node in nodes)
            {
                comicSeriesInfos.Add(node.GetAttributeValue("href", string.Empty));
            }
            return comicSeriesInfos;
        }
        
        // TODO: Fix string error regarding titels with (') in the name.
        public void ParseURLS(string webPage)
        {
            Series_Info series_Info = new();
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webPage);
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//div/ul/li[1]");

            series_Info.Name = htmlDocument.DocumentNode.SelectSingleNode("//h1").InnerText.Trim();
            series_Info.Id = int.Parse(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='bookmark']").GetAttributeValue("data-id", "-999"));
            series_Info.URL = node.SelectSingleNode("./a").GetAttributeValue("href", string.Empty);
            series_Info.ChapterName = node.GetAttributeValue("data-num", "-999");

            Console.WriteLine(series_Info.Name);
            Console.WriteLine(series_Info.Id);
            Console.WriteLine(series_Info.URL);
            Console.WriteLine(series_Info.ChapterName);
        }
    }
}