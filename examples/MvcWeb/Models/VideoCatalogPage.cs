using Piranha.AttributeBuilder;
using Piranha.Models;

namespace MvcWeb.Models
{
  [PageType(Title = "Video Catalog page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/video-catalog")]
    //[ContentTypeRoute(Title = "Default", Route = "/faq")]
    public class VideoCatalogPage : Page<VideoCatalogPage>
  {
  }
}