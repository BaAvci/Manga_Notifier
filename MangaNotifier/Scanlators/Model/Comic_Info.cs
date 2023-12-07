using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier.Scanlators.Model
{
    /// <summary>
    /// The model for all the Information about a single comic
    /// </summary>
    public class Comic_Info
    {
        public string? Scanlator { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public string? ChapterName { get; set; }

    }
}
