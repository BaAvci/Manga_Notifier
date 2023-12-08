using Manga_Notifier;
using Manga_Notifier.Scanlators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Unit_Tests.Crawling
{
    public class DiscordBot_Tests
    {
        private readonly Discord_Bot bot;
        private SourceView_Path_finder path_Finder;
        private Asurascans asurascans;
        private ulong channelID = 1182733801194016818;
        public DiscordBot_Tests() 
        {
            path_Finder = new SourceView_Path_finder();
            asurascans = new Asurascans("https://asuratoon.com/manga/?order=update");

            bot = new Discord_Bot();
        }

        [Fact]
        public async void Test()
        {
            var webpage = File.ReadAllText(path_Finder.AsuraSingle1ComicPath);
            asurascans.ParseURLS(webpage);
            await bot.StartAsync(channelID);
            await bot.Announce(scanlator: asurascans.SeriesInfo[0].Scanlator,
                               name: asurascans.SeriesInfo[0].Name,
                               chapter: asurascans.SeriesInfo[0].ChapterName,
                               link: asurascans.SeriesInfo[0].URL);
            var msg = await bot.Read();
            var result = msg.CleanContent.Split("\r\n");
            await bot.Delete();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
            Assert.Contains("Asuratoon",result[0]);
            Assert.Contains("SSS-Class Suicide Hunter", result[1]);
            Assert.Contains("Chapter 103", result[2]);
            Assert.Contains("https://asuratoon.com/7117659858-sss-class-suicide-hunter-chapter-103", result[4]);
        }

    }
}
