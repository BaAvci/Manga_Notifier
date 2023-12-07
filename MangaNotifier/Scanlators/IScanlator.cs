using Manga_Notifier.Scanlators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier.Scanlators
{
    public interface IScanlators
    {

        string Url { get; }
        List<Comic_Info> SeriesInfo { get; }

        /// <summary>
        /// Parses the library page and returns all available URLs
        /// </summary>
        /// <param name="responsBody"></param>
        /// <returns>List of all available URLs</returns>
        List<string> GetAllComics(string responsBody);

        /// <summary>
        /// Parses the source view of a single comic and creates a comic_info object
        /// </summary>
        /// <param name="webPage"></param>
        void ParseURLS(string webPage);
    }
}
