using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piranha.AttributeBuilder;
using Piranha.Models;


namespace RazorWeb.Models 
{

    /// <summary>
    /// Simplest possible page type could look like. 
    /// This page types doesn't provide anything other than the main 
    /// content area which is made up of blocks.
    /// UseExcerpt is true.IsePrimaryImage is true
    /// By default, all page request are rewritten to the route /page. 
    /// Since you want to load different model types for your pages, and often render them by 
    /// different views or pages you need to specify which route should handle your Page type. 
    /// Let's say we have a page that also displays a hero.
    /// </summary>
    [PageType(Title ="Simple Page")]
    public class SimplePage : Page<SimplePage>
    {

    }
}
