using Manga_Notifier.Crawling;
using Manga_Notifier.Scanlators;
using Manga_Notifier.Scanlators.Model;
using System;
using System.Collections;

namespace Manga_Notifier
{
    internal class Program
    {
        private readonly ulong channelID = 1182731621737181225;
        private Discord_Bot.Bot bot;
        public static Task Main(string[] args) => new Program().StartMain();

        public async Task StartMain()
        {
            Crawler crawler = new();
            List<IScanlators> scanlators = new()
            {
                //new Asurascans("https://asuratoon.com/manga/?order=update"),
                //new FlameScans("https://flamecomics.com/series/?order=update"),
                //new Reaperscans("https://reapercomics.com/latest/comics")
            };
            foreach(DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                if(de.Key.Equals("DISCORD_TOKEN"))
                {
                    Console.WriteLine($"{de.Key} = {de.Value}");
                }
            }
            bot = new Discord_Bot.Bot(channelID);
            await bot.StartBot();
            foreach(var scans in scanlators)
            {
                crawler.Crawl(scans);
                foreach(var series_Info in scans.SeriesInfo)
                {
                    Console.WriteLine(series_Info.Scanlator);
                    Console.WriteLine(series_Info.Name);
                    Console.WriteLine(series_Info.URL);
                    Console.WriteLine(series_Info.ChapterName);
                    await bot.Announce(series_Info.Scanlator, series_Info.Name, series_Info.ChapterName, series_Info.URL);
                }
                Thread.Sleep(5000);
            }
            await bot.StopBot();
        }
    }
}