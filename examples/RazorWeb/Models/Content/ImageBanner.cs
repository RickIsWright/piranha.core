/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/tidyui/coreweb
 *
 */

using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Extend.Fields.Settings;
using Piranha.Models;

namespace RazorWeb.Models
{
    /// <summary>
    /// Image Banner
    /// </summary>
    [ContentType(Title = "Image Banner")]
    public class ImageBanner : Banner<ImageBanner>
    {
        public class ImageBannerBody
        {
            [Field(Title = "Banner Image", Options = FieldOption.HalfWidth)]
            public ImageField Image { get; set; }

            [Field(Title = "Banner Title", Options = FieldOption.HalfWidth)]
            [StringFieldSettings(MaxLength = 64)]
            public StringField Title { get; set; }

            [Field]
            public HtmlField Body { get; set; }
        }

        [Region(Title = "Main Content")]
        public ImageBannerBody Body { get; set; }
    }
}
