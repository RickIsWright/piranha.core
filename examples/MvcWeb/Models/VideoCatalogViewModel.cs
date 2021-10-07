using System.Collections.Generic;

namespace MvcWeb.Models
{
    public class VideoCatalogViewModel
    {
        public VideoCatalogPage VideoCatalogPage { get; set; }
       
        public IEnumerable<VideoItem> VideoItems { get; set; }
    }
}