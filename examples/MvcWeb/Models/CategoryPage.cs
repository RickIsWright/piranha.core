using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace MvcWeb.Models
{
  [PageType(Title = "Category Page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/catalog-category")]
  public class CategoryPage : Page<CategoryPage>
  {
    [Region(Title = "Category Detail")]
    [RegionDescription("The details for this category.")]
    public CategoryRegion CategoryDetail { get; set; }

    [Region(Title = "Category SubItems")]
    [RegionDescription("The SubItems for this category.")]
    public IList<ProductRegion> Products
      { get; set; } = new List<ProductRegion>();
  }
}