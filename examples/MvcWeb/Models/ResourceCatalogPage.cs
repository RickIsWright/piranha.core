using Piranha.AttributeBuilder;
using Piranha.Models;

namespace MvcWeb.Models
{
  [PageType(Title = "Resource Catalog page", UseBlocks = false)]
  [ContentTypeRoute(Title = "Default", Route = "/resource-catalog")]
    //[ContentTypeRoute(Title = "Default", Route = "/faq")]
    public class ResourceCatalogPage : Page<ResourceCatalogPage>
  {
  }
}