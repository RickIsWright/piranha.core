using Piranha.Extend;
using Piranha.Extend.Fields;

namespace MvcWeb.Models
{
    public class VideoCategoryRegion
    {

        [Field(Title = "Video Title")]
        public TextField VideoTitle { get; set; }

        [Field]
        public HtmlField Description { get; set; }

        [Field(Title = "Video Cover Image")]
        public ImageField VideoImage { get; set; }

        [Field(Title = "Optional Video ID")]
        public NumberField VideoID { get; set; }
    }
}