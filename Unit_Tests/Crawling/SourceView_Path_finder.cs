using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Crawling
{
    internal class SourceView_Path_finder
    {
        public readonly string AsuraAllComicPath;
        public readonly string AsuraSingle1ComicPath;
        public readonly string AsuraSingle2ComicPath;
        public readonly string AsuraSingle3ComicPath;

        public readonly string FlameAllComicPath;
        public readonly string FlameSingle1ComicPath;
        public readonly string FlameSingle2ComicPath;
        public readonly string FlameSingle3ComicPath;

        public SourceView_Path_finder()
        {
            AsuraAllComicPath = GetFileViews("Asurascans", "Manga – Asura Scans.html");
            AsuraSingle1ComicPath = GetFileViews("Asurascans", "SSS-Class Suicide Hunter – Asura Scans.html");
            AsuraSingle2ComicPath = GetFileViews("Asurascans", "Terminally-Ill Genius Dark Knight – Asura Scans.html");
            AsuraSingle3ComicPath = GetFileViews("Asurascans", "The Reincarnated Assassin is a Genius Swordsman – Asura Scans.html");

            FlameAllComicPath = GetFileViews("Flamescans", "Manga Archive - Flame Comics.html");
            FlameSingle1ComicPath = GetFileViews("Flamescans", "In the Night Consumed by Blades, I Walk - Flame Comics.html");
            FlameSingle2ComicPath = GetFileViews("Flamescans", "Is This Hero for Real_ - Flame Comics.html");
            FlameSingle3ComicPath = GetFileViews("Flamescans", "I'll be Taking a Break for Personal Reasons - Flame Comics.html");
        }
        public string GetRootPath()
        {
            var pwd = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            return pwd.Parent.Parent.Parent.FullName;
        }

        public string GetSourceViews()
        {
            return Path.Join(GetRootPath(), "SourceViews");
        }

        public string GetFileViews(params string[] fragments)
        {
            var first = new string[] { GetSourceViews() };
            return Path.Join(first.Concat(fragments).ToArray());
        }

    }
}
