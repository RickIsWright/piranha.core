using System;

namespace MvcWeb.Models
{
    public class CategoryItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PageUrl { get; set; }
        public string ImageUrl { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

    }
}