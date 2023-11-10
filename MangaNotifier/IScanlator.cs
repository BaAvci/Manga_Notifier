using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Notifier
{
    interface IScanlators
    {
        List<string> GetAllComics(string responsBody);
        void ParseURLS(string webPage);
    }
}
