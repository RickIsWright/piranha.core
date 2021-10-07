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
using Piranha.Extend.Fields.Settings;
using Piranha.Models;

namespace RazorWeb.Models.Regions
{
    /// <summary>
    /// Test Field with all field types.
    /// </summary>
    public class AllFields
    {
        public enum StyleType
        {
            Standard,
            Wide,
            Narrow
        }

        [Field]
        public DataSelectField<PageItem> MyPage { get; set; }

        [Field(Placeholder = "PlaceHolder Text for AudioField.")]
        public AudioField Audio { get; set; }

        [Field(Placeholder = "PlaceHolder Text for CheckBoxField")]
        public CheckBoxField CheckBox { get; set; }

        [Field(Placeholder = "PlaceHolder Text for ColorField")]
        [ColorFieldSettings(DisallowInput = true)]
        public ColorField Color { get; set; }

        [Field(Placeholder = "Select any content from the application")]
        public ContentField Content { get; set; }

        [Field(Placeholder = "Select any banner content from the application")]
        [ContentFieldSettings(Group = "Banners")]
        public ContentField Banner { get; set; }

        [Field(Placeholder = "Select any product content from the application")]
        [ContentFieldSettings(Group = "Products")]
        public ContentField Product { get; set; }

        [Field(Placeholder = "PlaceHolder Text for DateField")]
        public DateField Date { get; set; }

        [Field(Placeholder = "PlaceHolder Text for HtmlField")]
        public HtmlField Html { get; set; }

        [Field(Options = FieldOption.HalfWidth, Placeholder = "PlaceHolder Text for DocumentField")]
        public DocumentField Document { get; set; }

        [Field(Options = FieldOption.HalfWidth, Placeholder = "PlaceHolder Text for ImageField")]
        public ImageField Image { get; set; }

        [Field(Placeholder = "PlaceHolder Text for MediaField")]
        public MediaField Media { get; set; }

        [Field(Placeholder = "PlaceHolder Text for VideoField.")]
        [FieldDescription("Describe Video <strong>and video emphasis</strong> with final info here.")]
        public VideoField Video { get; set; }

        [Field(Placeholder = "PlaceHolder Text for MarkdownField")]
        public MarkdownField Markdown { get; set; }

        [Field(Placeholder = "PlaceHolder Text for NumberField int or null")]
        public NumberField Number { get; set; }

        [Field(Placeholder = "PlaceHolder Text for PageField")]
        public PageField Page { get; set; }

        [Field(Placeholder = "PlaceHolder Text for PostField")]
        public PostField Post { get; set; }

        [Field(Placeholder = "PlaceHolder Text for StringField which is always a string")]
        [StringFieldSettings(MaxLength = 8)]
        public StringField String { get; set; }

        [Field(Placeholder = "PlaceHolder Text for TextField.")]
        public TextField Text { get; set; }

        // Select Field Does Not Have a placeholder
        [Field]
        public SelectField<StyleType> Style { get; set; }
    }
}
