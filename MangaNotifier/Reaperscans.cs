using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Manga_Notifier
{
    public class Reapoerscans : IScanlators
    {
        private readonly string url;
        private List<Series_Info> seriesInfo;
        public Reapoerscans(string url)
        {
            this.url = url;
            seriesInfo = new List<Series_Info>();
        }

        public List<string> GetAllComics(string responsBody)
        {
            HtmlDocument htmlDocument = new();
            List<string> comicSeriesInfos = new();
            htmlDocument.LoadHtml(responsBody);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, \"grid\")][1]/div/div/a/@href");
            foreach (HtmlNode node in nodes)
            {
                comicSeriesInfos.Add(node.GetAttributeValue("href", string.Empty));
            }
            return comicSeriesInfos;
        }

        public void ParseURLS(string webPage)
        {
            Series_Info series_Info = new();
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webPage);
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//li/a[1]");

            series_Info.Name = htmlDocument.DocumentNode.SelectSingleNode("//h1[1]").InnerText.Trim();
            var comicURL = node.GetAttributeValue("href", "-999");
            Match m = Regex.Match(comicURL, "([0-9]+)");
            series_Info.Id = int.Parse(m.Value);
            series_Info.URL = node.GetAttributeValue("href", string.Empty);
            series_Info.ChapterName = node.SelectSingleNode(".//p[1]").InnerText.Trim();

            Console.WriteLine(series_Info.Name);
            Console.WriteLine(series_Info.Id);
            Console.WriteLine(series_Info.URL);
            Console.WriteLine(series_Info.ChapterName);
        }
    }
}
