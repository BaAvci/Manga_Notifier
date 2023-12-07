using Manga_Notifier.Scanlators;
using System;
using System.Security.Claims;
using Unit_Tests.Crawling;

namespace Unit_Tests
{
    public class SourceViewParserTest
    {

        readonly SourceView_Path_finder testFiles;

        readonly IScanlators asurascans;
        readonly IScanlators flamescans;

        public SourceViewParserTest() 
        {
            testFiles = new SourceView_Path_finder();

            asurascans = new Asurascans("https://asuratoon.com/manga/?order=update"); 
            flamescans = new FlamScans("https://flamecomics.com/series/?order=update");
        }

        [Fact]
        public void ParseAllComicsAsura()
        {
            var webpage = File.ReadAllText(testFiles.AsuraAllComicPath);
            var comicamount = asurascans.GetAllComics(webpage);
            Assert.Equal(20, comicamount.Count);
        }

        [Fact]
        public void ParseSingleComicAsura()
        {
            var webpage = File.ReadAllText(testFiles.AsuraSingle1ComicPath);
            asurascans.ParseURLS(webpage);
            Assert.Single(asurascans.SeriesInfo);
            Assert.Equal("Asuratoon",asurascans.SeriesInfo[0].Scanlator);
            Assert.Equal("SSS-Class Suicide Hunter", asurascans.SeriesInfo[0].Name);
            Assert.Equal("https://asuratoon.com/7117659858-sss-class-suicide-hunter-chapter-103/", asurascans.SeriesInfo[0].URL);
            Assert.Equal("Chapter 103", asurascans.SeriesInfo[0].ChapterName);
            Assert.Equal(31591, asurascans.SeriesInfo[0].Id);
        }

        [Fact]
        public void ParseMultipleSingleComicAsura()
        {
            asurascans.ParseURLS(File.ReadAllText(testFiles.AsuraSingle1ComicPath));
            asurascans.ParseURLS(File.ReadAllText(testFiles.AsuraSingle2ComicPath));
            asurascans.ParseURLS(File.ReadAllText(testFiles.AsuraSingle3ComicPath));

            Assert.Equal(3, asurascans.SeriesInfo.Count);

            Assert.Equal("Asuratoon", asurascans.SeriesInfo[0].Scanlator);
            Assert.Equal("Asuratoon", asurascans.SeriesInfo[1].Scanlator);
            Assert.Equal("Asuratoon", asurascans.SeriesInfo[2].Scanlator);

            Assert.Equal("SSS-Class Suicide Hunter", asurascans.SeriesInfo[0].Name);
            Assert.Equal("Terminally-Ill Genius Dark Knight", asurascans.SeriesInfo[1].Name);
            Assert.Equal("The Reincarnated Assassin is a Genius Swordsman", asurascans.SeriesInfo[2].Name);

            Assert.Equal("https://asuratoon.com/7117659858-sss-class-suicide-hunter-chapter-103/", asurascans.SeriesInfo[0].URL);
            Assert.Equal("https://asuratoon.com/7117659858-terminally-ill-genius-dark-knight-chapter-33/", asurascans.SeriesInfo[1].URL);
            Assert.Equal("https://asuratoon.com/7117659858-the-reincarnated-assassin-is-a-genius-swordsman-chapter-28/", asurascans.SeriesInfo[2].URL);

            Assert.Equal("Chapter 103", asurascans.SeriesInfo[0].ChapterName);
            Assert.Equal("Chapter 33", asurascans.SeriesInfo[1].ChapterName);
            Assert.Equal("Chapter 28", asurascans.SeriesInfo[2].ChapterName);

            Assert.Equal(31591, asurascans.SeriesInfo[0].Id);
            Assert.Equal(219332, asurascans.SeriesInfo[1].Id);
            Assert.Equal(245591, asurascans.SeriesInfo[2].Id);
        }

        [Fact]
        public void ParseAllComicsFlame()
        {
            var webpage = File.ReadAllText(testFiles.FlameAllComicPath);
            var comicamount = flamescans.GetAllComics(webpage);
            Assert.Equal(24, comicamount.Count);
        }

        [Fact]
        public void ParseSingleComicFlame()
        {
            var webpage = File.ReadAllText(testFiles.FlameSingle1ComicPath);
            flamescans.ParseURLS(webpage);
            Assert.Single(flamescans.SeriesInfo);
            Assert.Equal("Flamecomics", flamescans.SeriesInfo[0].Scanlator);
            Assert.Equal("In the Night Consumed by Blades, I Walk", flamescans.SeriesInfo[0].Name);
            Assert.Equal("https://flamecomics.com/in-the-night-consumed-by-blades-i-walk-chapter-87/", flamescans.SeriesInfo[0].URL);
            Assert.Equal("87", flamescans.SeriesInfo[0].ChapterName);
            Assert.Equal(91628, flamescans.SeriesInfo[0].Id);
        }

        [Fact]
        public void ParseMultipleSingleComicFlame()
        {
            flamescans.ParseURLS(File.ReadAllText(testFiles.FlameSingle1ComicPath));
            flamescans.ParseURLS(File.ReadAllText(testFiles.FlameSingle2ComicPath));
            flamescans.ParseURLS(File.ReadAllText(testFiles.FlameSingle3ComicPath));

            Assert.Equal(3, flamescans.SeriesInfo.Count);

            Assert.Equal("Flamecomics", flamescans.SeriesInfo[0].Scanlator);
            Assert.Equal("Flamecomics", flamescans.SeriesInfo[1].Scanlator);
            Assert.Equal("Flamecomics", flamescans.SeriesInfo[2].Scanlator);

            Assert.Equal("In the Night Consumed by Blades, I Walk", flamescans.SeriesInfo[0].Name);
            Assert.Equal("Is This Hero for Real?", flamescans.SeriesInfo[1].Name);
            Assert.Equal("I’ll be Taking a Break for Personal Reasons", flamescans.SeriesInfo[2].Name);

            Assert.Equal("https://flamecomics.com/in-the-night-consumed-by-blades-i-walk-chapter-87/", flamescans.SeriesInfo[0].URL);
            Assert.Equal("https://flamecomics.com/is-this-hero-for-real-chapter-95/", flamescans.SeriesInfo[1].URL);
            Assert.Equal("https://flamecomics.com/ill-be-taking-a-break-for-personal-reasons-chapter-78/", flamescans.SeriesInfo[2].URL);

            Assert.Equal("87", flamescans.SeriesInfo[0].ChapterName);
            Assert.Equal("95", flamescans.SeriesInfo[1].ChapterName);
            Assert.Equal("78", flamescans.SeriesInfo[2].ChapterName);

            Assert.Equal(91628, flamescans.SeriesInfo[0].Id);
            Assert.Equal(71331, flamescans.SeriesInfo[1].Id);
            Assert.Equal(108114, flamescans.SeriesInfo[2].Id);
        }
    }
}