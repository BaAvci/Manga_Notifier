using Manga_Notifier.Crawling;
using Manga_Notifier.Scanlators;
using Manga_Notifier.Scanlators.Model;
using System;

namespace Manga_Notifier // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private readonly ulong channelID = 1182731621737181225;
        public static Task Main(string[] args) => new Program().StartMain();

        public async Task StartMain()
        {
            Crawler crawler = new();
            List<IScanlators> scanlators = new()
            {
                new Asurascans("https://asuratoon.com/manga/?order=update"),
                new FlamScans("https://flamecomics.com/series/?order=update"),
                //new Reaperscans("https://reaperscans.com/latest/comics")
            };

            var bot = new Discord_Bot();
            await bot.StartAsync(channelID);
            foreach (IScanlators scans in scanlators)
            {
                crawler.Crawl(scans);
                foreach (var series_Info in scans.SeriesInfo)
                {
                    Console.WriteLine(series_Info.Scanlator);
                    Console.WriteLine(series_Info.Name);
                    Console.WriteLine(series_Info.URL);
                    Console.WriteLine(series_Info.ChapterName);
                    await bot.Announce(series_Info.Scanlator, series_Info.Name, series_Info.ChapterName, series_Info.URL);
                }
                Thread.Sleep(5000);
            }
            await bot.CloseAsync();
        }
    }
}