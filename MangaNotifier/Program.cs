using Manga_Notifier;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Crawler crawler = new();
            IScanlators scanlators = new Asurascans("https://asuratoon.com/manga/?order=update");
            IScanlators scanlators2 = new FlamScans("https://flamecomics.com/series/?order=update");
            IScanlators scanlators3 = new Reapoerscans("https://reaperscans.com/latest/comics");

            await crawler.GetWebPage("https://reaperscans.com/latest/comics", scanlators3);
            //await crawler.GetWebPage("https://asuratoon.com/manga/?order=update", scanlators);
            //await crawler.GetWebPage("https://flamecomics.com/series/?order=update", scanlators2);
        }
    }
}