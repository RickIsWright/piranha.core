using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piranha.AttributeBuilder;
using Piranha.Models;

namespace RazorWeb.Models
{
    /// <summary>
    /// Defining an archive page is equally simple, the only additional 
    /// thing you need to do is to set IsArchive = true.
    /// The page type is imported in the same way as regular pages.
    /// </summary>
    [PageType(Title ="Simple Archive",IsArchive = true)]
    public class SimpleArchive:Page<SimpleArchive>
    {
    }
}
