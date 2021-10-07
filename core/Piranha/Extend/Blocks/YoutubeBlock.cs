using System;
using System.Collections.Generic;
using System.Text;
using Piranha.Extend;
using Piranha.Extend.Fields;
namespace Piranha.Extend.Blocks
{
    [BlockType(Name ="YouTube Video",Category ="Media", Icon ="fa fa-youtube")]
    public class YoutubeBlock : Block
    {
        [Field(Title ="YouTube Video Link Url: ")]
        public TextField YouTubeVideoUrl { get; set; }
    }
}
