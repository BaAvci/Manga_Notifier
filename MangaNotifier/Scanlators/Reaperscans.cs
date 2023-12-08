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
        private readonly List<Comic_Info> seriesInfo;
        public Reaperscans(string url)
        {
            this.url = url;
            seriesInfo = new List<Comic_Info>();
        }

        public List<string> GetAllComics(string responsBody)
        {
            HtmlDocument htmlDocument = new();
            List<string> comicSeriesInfos = new();
            htmlDocument.LoadHtml(responsBody);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, \"grid\")][1]/div/div/a/@href");
            if (nodes is not null)
            {
                foreach(var node in nodes)
                {
                    comicSeriesInfos.Add(node.GetAttributeValue("href", string.Empty));
                }
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
            var scanlator = new Regex("(?<=:\\/\\/)(?:.*)(?=\\.)").Match(url).Value;

            seriesInfo.Add(new Comic_Info
            {
                Scanlator = char.ToUpper(scanlator[0]) + scanlator.Substring(1),
                Name = htmlDocument.DocumentNode.SelectSingleNode("//h1[1]").InnerText.Trim(),
                Id = int.Parse(m.Value),
                URL = node.GetAttributeValue("href", string.Empty),
                ChapterName = node.SelectSingleNode(".//p[1]").InnerText.Trim()
            });
        }

        public string Url => url;
        public List<Comic_Info> SeriesInfo => seriesInfo;
    }
}
