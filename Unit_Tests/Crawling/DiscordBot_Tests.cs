using Manga_Notifier.Scanlators;
using System.Collections;
using Xunit.Abstractions;

namespace Unit_Tests.Crawling
{
    [Collection("LaunchSettingFixture")]
    public class DiscordBot_Tests :IClassFixture<LaunchSettingFixture>
    {
        private Discord_Bot.Bot bot;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly SourceView_Path_finder path_Finder;
        private readonly Asurascans asurascans;
        private readonly ulong channelID = 1182733801194016818;
        private readonly LaunchSettingFixture _fixture;

        public DiscordBot_Tests(LaunchSettingFixture fixture, ITestOutputHelper testOutputHelper) 
        {
            path_Finder = new SourceView_Path_finder();
            asurascans = new Asurascans("https://asuratoon.com/manga/?order=update");
            _testOutputHelper = testOutputHelper;
            _fixture = fixture;
            bot = new Discord_Bot.Bot(channelID);
        }

        [Fact]
        public async void Test()
        {
            var webpage = File.ReadAllText(path_Finder.AsuraSingle1ComicPath);
            asurascans.ParseURLS(webpage);
            await bot.StartBot();
            await bot.Announce(scanlator: asurascans.SeriesInfo[0].Scanlator,
                               name: asurascans.SeriesInfo[0].Name,
                               chapter: asurascans.SeriesInfo[0].ChapterName,
                               link: asurascans.SeriesInfo[0].URL);
            var msg = await bot.Read();
            var result = msg.CleanContent.Split("\r\n");
            await bot.Delete();

            Assert.NotNull(result);
            Assert.Equal(5, result.Length);
            Assert.Contains("Asuratoon",result[0]);
            Assert.Contains("SSS-Class Suicide Hunter", result[1]);
            Assert.Contains("Chapter 103", result[2]);
            Assert.Contains("https://asuratoon.com/7117659858-sss-class-suicide-hunter-chapter-103", result[4]);
            await bot.StopBot();
        }

    }
}
