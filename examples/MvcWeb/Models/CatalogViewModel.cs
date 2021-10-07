using System.Collections.Generic;

namespace MvcWeb.Models
{
  public class CatalogViewModel
  {
    public CatalogPage CatalogPage { get; set; }
    public IEnumerable<CategoryItem> Categories { get; set; }
  }
}