using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace MvcWeb.Models
{
  [PageType(Title = "FAQ Page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/faq")]
  public class FaqPage : Page<FaqPage>
  {
        [Region(Title = "FAQ Details")]
        [RegionDescription("Details for this FAQ category.")]
        public ResourceCategoryRegion ResourceCategoryDetail { get; set; }
    }
}