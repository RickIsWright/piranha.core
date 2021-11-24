using Piranha.Extend;
using Piranha.Extend.Fields;

namespace MvcWeb.Models
{
    public class ResourceCategoryRegion
    {

        [Field(Title = "Resource Title")]
        public TextField CategoryName { get; set; }

        [Field]
        public HtmlField Description { get; set; }

        [Field(Title = "Resource Image")]
        public ImageField CategoryImage { get; set; }

        [Field(Title = "Optional Resource ID")]
        public NumberField CategoryID { get; set; }



        //[Field(Title ="Show Child Items")]
        //public CheckBoxField ShowChildItems { get; set; }
    }
}