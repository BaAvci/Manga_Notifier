using Manga_Notifier;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Crawler crawler = new();
            await crawler.GetWebPage();
        }
    }
}