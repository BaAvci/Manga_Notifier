﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier
{
    public interface IScanlators
    {

        string Url { get; }
        List<Series_Info> SeriesInfo { get; }

        List<string> GetAllComics(string responsBody);
        void ParseURLS(string webPage);
    }
}
