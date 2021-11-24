using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace MvcWeb.Models
{
    public class SubitemRegion
    {
        [Field(Title = "Subitem ID")]
        public NumberField SubitemID { get; set; }

        [Field(Title = "Subitem Name", Options = FieldOption.HalfWidth)]
        public TextField SubitemName { get; set; }

        [Field(Title = "Subitem Title", Options = FieldOption.HalfWidth)]
        public StringField SubitemTitle { get; set; }

        [Field(Title = "Subitem Description")]
        public StringField SubitemDescription { get; set; }

        [Field(Title = "Subitem Created")]
        public DateField SubitemCreated { get; set; }

        [Field(Title = "Subitem Created By")]
        public StringField SubitemCreatedBy { get; set;  }


    }
}