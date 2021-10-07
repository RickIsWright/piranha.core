using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace MvcWeb.Models
{
    // product
    public class ResourceListRegion
    {
        

        [Field(Title = "Sub-resource Title")]
        public TextField SubResourceTitle { get; set; }
        [Field(Title = "Sub-resource Description")]
        public TextField SubResourceDescription { get; set; }

        [Field(Title = "SubResource Value 1", Options = FieldOption.HalfWidth)]
        public StringField SubResourceValue1 { get; set; }

        [Field(Title = "SubResource #Value 2", Options = FieldOption.HalfWidth)]
        public NumberField SubResourceValue2 { get; set; }

        [Field(Title = "Optional ResourceList ID")]
        public NumberField ResourceListID { get; set; }
    }
}