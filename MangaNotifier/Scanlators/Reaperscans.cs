using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Manga_Notifier.Scanlators.Model;

namespace Manga_Notifier.Scanlators
{
    public class Reaperscans : IScanlators
    {
        private readonly string url;
        private List<Series_Info> seriesInfo;
        public Reaperscans(string url)
        {
            this.url = url;
            seriesInfo = new List<Series_Info>();
        }

        public List<string> GetAllComics(string responsBody)
        {
            HtmlDocument htmlDocument = new();
            List<string> comicSeriesInfos = new();
            htmlDocument.LoadHtml(responsBody);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, \"grid\")][1]/div/div/a/@href");
            foreach(var node in nodes)
            {
                comicSeriesInfos.Add(node.GetAttributeValue("href", string.Empty));
            }
            return comicSeriesInfos;
        }

        public void ParseURLS(string webPage)
        {
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webPage);
            var node = htmlDocument.DocumentNode.SelectSingleNode("//li/a[1]");
            var comicURL = node.GetAttributeValue("href", "-999");
            var m = Regex.Match(comicURL, "([0-9]+)");

            seriesInfo.Add(new Series_Info
            {
                Name = htmlDocument.DocumentNode.SelectSingleNode("//h1[1]").InnerText.Trim(),
                Id = int.Parse(m.Value),
                URL = node.GetAttributeValue("href", string.Empty),
                ChapterName = node.SelectSingleNode(".//p[1]").InnerText.Trim()
            });
        }

        public string Url => url;
        public List<Series_Info> SeriesInfo => seriesInfo;
    }
}
