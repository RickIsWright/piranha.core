using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using MvcWeb.Models;
using MvcWeb.Models.Regions;
using Piranha.Extend.Fields;

namespace MvcWeb.Models
{
    [PageType(Title = "Catalog Page", UseBlocks = false)]
    [ContentTypeRoute(Title = "Default", Route = "/catalog")]
    public class CatalogPage : Page<CatalogPage>
    {
        // since a Region class cannot contain only one field, declare the field type directly in the page. R. Wright/
        // currently, not used. Using hidden for the child item instead
        //[Region(Title = "Show Child Items in Menu", Display = RegionDisplayMode.Setting)]
        //[RegionDescription("When checked, a sub menu item will be generated for this item if it has child items.")]
        //public CheckBoxField YesNo { get; set; }
    }
}