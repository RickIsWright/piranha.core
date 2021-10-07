using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using RazorWeb.Models;

namespace RazorWeb.Models
{
    /// <summary>
    /// By adding the ContentTypeRouteAttribute to your page type,
    /// all requests for pages of this page type will now be routed to /heropage.
    /// Let's say we would also like to use our Hero Page as the Startpage of the site, 
    /// but we might want to handle it differently by adding some content from another system,
    /// or send it to a different view. We can then just add a second PageRouteAttribute to our class.
    /// By adding a second route the page settings in the manager will now show a dropdown 
    /// where the editor can select which route the current page should use.
    /// By default pages can use all of the available Block Types in their main content. If you want 
    /// to limit the available blocks for a certain page type this can be done by adding one or more
    /// BlockItemTypeAttribute to the class
    /// </summary>
    [PageType(Title ="Hero Page2", UseBlocks =false)]
    [ContentTypeRoute(Title ="Default", Route ="/heropage")]
    [ContentTypeRoute(Title = "Start Page", Route = "/startpage")]
    // [BlockItemType(typeof(Piranha.Extend.Blocks.HtmlBlock))] // limit to HtmlBlock
    public class HeroPage : Page<HeroPage>
    {

        //public class HeroRegion
        //{
        //    [Field]
        //    public StringField Title { get; set; }

        //    [Field]
        //    public ImageField Image { get; set; }

        //    [Field]
        //    public TextField Body { get; set; }
        //} // eoc HeroRegion

        [Region]
        [RegionDescription("The HeroB Region is shown on the top of your page")]
        public Regions.HeroB Hero { get; set; }

        [Region(Title = "Images", ListTitle = "Title", ListPlaceholder = "New image", Icon = "fas fa-images", ListExpand = false)]
        public IList<ImageField> Images { get; set; }

        public PostInfo LatestPost { get; internal set; }
    } // eoc HeroPage
}
