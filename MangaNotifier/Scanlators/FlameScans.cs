using HtmlAgilityPack;
using Manga_Notifier.Scanlators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier.Scanlators
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
            var nodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='bs']/div/a");
            foreach(var node in nodes)
            {
                comicSeriesInfos.Add(node.GetAttributeValue("href", string.Empty));
            }
            return comicSeriesInfos;
        }

        // TODO: Fix string error regarding titels with (') in the name.
        public void ParseURLS(string webPage)
        {
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webPage);
            var node = htmlDocument.DocumentNode.SelectSingleNode("//div/ul/li[1]");
            var name = htmlDocument.DocumentNode.SelectSingleNode("//h1").InnerText.Trim();
            seriesInfo.Add(new Series_Info
            {
                Name = System.Web.HttpUtility.HtmlDecode(name),
                Id = int.Parse(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='bookmark']").GetAttributeValue("data-id", "-999")),
                URL = node.SelectSingleNode("./a").GetAttributeValue("href", string.Empty),
                ChapterName = node.GetAttributeValue("data-num", "-999")
            });

        }

        public string Url => url;
        public List<Series_Info> SeriesInfo => seriesInfo;
    }
}