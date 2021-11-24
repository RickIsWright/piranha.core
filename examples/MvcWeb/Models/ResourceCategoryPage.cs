using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace MvcWeb.Models
{
  [PageType(Title = "Resource Category Page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/resource-catalog-resource-category")]
  [ContentTypeRoute(Title = "Default", Route = "/faq-resource-category")]
    public class ResourceCategoryPage : Page<ResourceCategoryPage>
  {
    [Region(Title = "Resource Category Detail")]
    [RegionDescription("Details for this resource category.")]
    public ResourceCategoryRegion ResourceCategoryDetail { get; set; }

    [Region(Title = "Resource sublist")]
    [RegionDescription("The optional sublist for this category.")]
    public IList<ResourceListRegion> ResourceList
      { get; set; } = new List<ResourceListRegion>();
  }
}