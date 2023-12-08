using Manga_Notifier.Crawling;
using Manga_Notifier.Scanlators;
using Manga_Notifier.Scanlators.Model;
using System;

namespace Manga_Notifier // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crawler crawler = new();

            List<IScanlators> scanlators = new()
            {
                new Asurascans("https://asuratoon.com/manga/?order=update"),
                new FlameScans("https://flamecomics.com/series/?order=update"),
                new Reaperscans("https://reapercomics.com/latest/comics")
            };

            foreach (IScanlators scans in scanlators)
            {
                crawler.Crawl(scans);
                foreach (var series_Info in scans.SeriesInfo)
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