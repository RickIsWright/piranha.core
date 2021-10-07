using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace MvcWeb.Models
{
  public class VideoProductRegion
  {
    [Field(Title = "Video Product ID")]
    public NumberField VideoProductID { get; set; }

    [Field(Title = "Video Product Title")]
    public TextField VideoProductTitle { get; set; }

    [Field(Title = "Video Rating", Options = FieldOption.HalfWidth)]
    public StringField VideoRating { get; set; }

    [Field(Title = "Video Product Views", Options = FieldOption.HalfWidth)]
    public NumberField VideoProductViews { get; set; }
  }
}