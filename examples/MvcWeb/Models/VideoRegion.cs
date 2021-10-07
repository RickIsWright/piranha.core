using Piranha.Extend;
using Piranha.Extend.Fields;

namespace MvcWeb.Models
{
    public class VideoRegion
    {       

        [Field(Title = "Video Title")]
        public TextField VideoTitle { get; set; }

        [Field]
        public HtmlField Description { get; set; }

        [Field(Title = "Video Cover Image")]
        public ImageField VideoCoverImage { get; set; }

        [Field(Title = "Video ID")]
        public NumberField VideoID { get; set; }
    }
}