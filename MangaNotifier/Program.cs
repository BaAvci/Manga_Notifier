using Manga_Notifier;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Crawler crawler = new();

            List<IScanlators> scanlators = new()
            {
                new Asurascans("https://asuratoon.com/manga/?order=update"),
                new FlamScans("https://flamecomics.com/series/?order=update"),
                //new Reaperscans("https://reaperscans.com/latest/comics")
            };

            foreach (IScanlators scans in scanlators)
            {
                await crawler.GetWebPage(scans.Url, scans);
                foreach (Series_Info series_Info in scans.SeriesInfo)
                {
                    Console.WriteLine(series_Info.Name);
                    Console.WriteLine(series_Info.URL);
                    Console.WriteLine(series_Info.ChapterName);
                    Console.WriteLine(series_Info.Id);
                }
                Thread.Sleep(5000);
            }
        }
    }
}