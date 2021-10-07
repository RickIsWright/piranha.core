using System.Collections.Generic;

namespace MvcWeb.Models
{
    public class ResourceCatalogViewModel
    {
        public ResourceCatalogPage ResourceCatalogPage { get; set; }
        public FaqPage FaqPage { get; set; }
        public IEnumerable<ResourceCategoryItem> ResourceCategories { get; set; }
    }
}