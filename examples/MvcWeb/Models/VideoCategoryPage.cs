using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace MvcWeb.Models
{
  [PageType(Title = "Video Category Page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/video-category")]
  public class VideoCategoryPage : Page<VideoCategoryPage>
  {
    [Region(Title = "Video Category Detail")]
    [RegionDescription("The details for this video category.")]
    public VideoCategoryRegion CategoryDetail { get; set; }

    [Region(Title = "Related videos in this category")]
    [RegionDescription("Additional videos for this category.")]
    public IList<VideoProductRegion> Videos
      { get; set; } = new List<VideoProductRegion>();
  }
}