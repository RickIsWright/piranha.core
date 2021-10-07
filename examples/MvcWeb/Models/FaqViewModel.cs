using System.Collections.Generic;

namespace MvcWeb.Models
{
    public class FaqViewModel
    {
        public FaqPage FaqPage { get; set; }
        //public FaqPage FaqPage { get; set; }
        public IEnumerable<ResourceCategoryItem> ResourceCategories { get; set; }
    }
}