/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/tidyui/coreweb
 *
 */

using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace MvcWeb.Models.Regions
{
    /// <summary>
    /// Simple region for a yes no item.
    /// </summary>
    public class YesNoCheckBox
    {
        /// <summary>
        /// R. Wright
        /// Regions Class cannot be used if they contain only one field. The single field should be declared
        /// directly in the page.
        /// Gets/sets the checkbox to show the ChildItems. This item should be used in Regions that have child items.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth, Title ="Yes or No option")]
        public CheckBoxField ShowChildItems { get; set; }



        /// <summary>
        /// Gets/sets the subtitle.
        /// </summary>
        //[Field(Options = FieldOption.HalfWidth)]
        //public StringField SubTitle { get; set; }

        ///// <summary>
        ///// Gets/sets the optional teaser image.
        ///// </summary>
        //[Field]
        //public ImageField Image { get; set; }

        ///// <summary>
        ///// Gets/sets the main body.
        ///// </summary>
        //[Field]
        //public HtmlField Body { get; set; }
    }
}
