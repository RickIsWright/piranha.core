using System;
using System.Collections.Generic;
using System.Text;
using Piranha.Extend;
using Piranha.Extend.Fields;

/*R. Wright
  need to pick from fas for solid, far for regular, fal for light, or fab for brand.
 */

namespace Piranha.Extend.Blocks
{
    [BlockType(Name ="YouTube Video",Category ="Media", Icon ="fab fa-youtube")]
    public class YoutubeBlock : Block
    {
        [Field(Title ="YouTube Video Url")]
        public TextField YouTubeVideoUrl { get; set; }
    }
}
