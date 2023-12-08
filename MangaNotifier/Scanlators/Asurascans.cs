using HtmlAgilityPack;
using Manga_Notifier.Scanlators.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Manga_Notifier.Scanlators
{
    public class Asurascans : IScanlators
    {
        private readonly string url;
        private readonly List<Comic_Info> seriesInfo;
        public Asurascans(string url)
        {
            this.url = url;
            seriesInfo = new List<Comic_Info>();
        }

        public List<string> GetAllComics(string responsBody)
        {
            HtmlDocument htmlDocument = new();
            List<string> comicSeriesInfos = new();
            htmlDocument.LoadHtml(responsBody);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//div/div[@class='bsx']/a/@href");
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
            var node = htmlDocument.DocumentNode.SelectSingleNode("//li[1]/div/div/a[@href]");
            var scanlator = new Regex("(?<=:\\/\\/)(?:.*)(?=\\.)").Match(url).Value;
            seriesInfo.Add(new Comic_Info
            {
                Scanlator = char.ToUpper(scanlator[0]) + scanlator.Substring(1),
                Name = htmlDocument.DocumentNode.SelectSingleNode("//h1").InnerText.Trim(),
                Id = int.Parse(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='bookmark']").GetAttributeValue("data-id", "-999")),
                URL = node.GetAttributeValue("href", string.Empty),
                ChapterName = node.SelectSingleNode("./span[@class='chapternum']").InnerText.Trim()
            });
        }

        public string Url => url;
        public List<Comic_Info> SeriesInfo => seriesInfo;
    }
}
