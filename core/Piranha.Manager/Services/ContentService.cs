/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using Piranha.Manager.Models;
using Piranha.Services;
using System.Collections;
using System.Dynamic;
using System.ComponentModel.DataAnnotations;

namespace Piranha.Manager.Services
{
    public class ContentService
    {
        private readonly IApi _api;
        private readonly IContentFactory _factory;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <param name="factory">The content factory</param>
        public ContentService(IApi api, IContentFactory factory)
        {
            _api = api;
            _factory = factory;
        }

        /// <summary>
        /// Gets the list model.
        /// </summary>
        /// <param name="contentGroup">Name of the content group</param>
        /// <returns>Gets the list model.</returns>
        public async Task<ContentListModel> GetListAsync(string contentGroup)
        {
            var groups = contentGroup == null ?
                App.ContentGroups.ToList() :
                new List<ContentGroup>();
            var group = contentGroup == null ?
                App.ContentGroups.GetById(groups.First().Id) :
                App.ContentGroups.GetById(contentGroup);
            var types = App.ContentTypes.GetByGroupId(group.Id);
            var items = await _api.Content.GetAllAsync<ContentInfo>(group.Id);

            return new ContentListModel
            {
                Group = group,
                Groups = groups,
                Items = items
                    .Select(i => new ContentListModel.ContentItem
                        {
                            Id = i.Id,
                            Title = i.Title,
                            TypeId = i.TypeId,
                            Modified = i.LastModified.ToString("yyyy-MM-dd")
                        })
                    .ToList(),
                Types = types
                    .Select(t =>
                        new Models.Content.ContentTypeModel
                        {
                            Id = t.Id,
                            Title = t.Title,
                            AddUrl = $"manager/content/add/{t.Id}"
                        })
                    .ToList()
            };
        }

        /// <summary>
        /// Get the content edit model by contnet id
        /// </summary>
        /// <param name="id">The content id</param>
        /// <returns>Edit model</returns>
        public async Task<ContentEditModel> GetByIdAsync(Guid id)
        {
            var content = await _api.Content.GetByIdAsync<DynamicContent>(id);
            if (content != null)
            {
                var type =  App.ContentTypes.GetById(content.TypeId);
                var group = App.ContentGroups.GetById(type.Group);

                // Perform manager init
                await _factory.InitDynamicManagerAsync(content,
                    App.ContentTypes.GetById(content.TypeId));

                var model = Transform(content);

                model.TypeId = type.Id;
                model.TypeTitle = type.Title;
                model.GroupId = group.Id;
                model.GroupTitle = group.Title;

                model.State = ContentState.Published;

                return model;
            }

            return null;
        }

        /// <summary>
        /// Create new content based on content type id
        /// </summary>
        /// <param name="typeId">Content type id</param>
        /// <returns>Nee edit model</returns>
        public async Task<ContentEditModel> CreateAsync(string typeId)
        {
            var type =  App.ContentTypes.GetById(typeId);
            var group = App.ContentGroups.GetById(type.Group);
            var content = await _api.Content.CreateAsync<DynamicContent>(typeId);
            if (content != null)
            {
                content.Id = Guid.NewGuid();

                await _factory.InitDynamicManagerAsync(content, type);

                var model = Transform(content);

                model.TypeId = type.Id;
                model.TypeTitle = type.Title;
                model.GroupId = group.Id;
                model.GroupTitle = group.Title;
                model.State = ContentState.New;

                return model;
            }

            return null;
        }

        /// <summary>
        /// Save content
        /// </summary>
        /// <param name="model">The edit model</param>
        public async Task SaveAsync(ContentEditModel model)
        {
            var contentType = App.ContentTypes.GetById(model.TypeId);

            if (contentType != null)
            {
                if (model.Id == Guid.Empty)
                {
                    model.Id = Guid.NewGuid();
                }

                var content = await _api.Content.GetByIdAsync(model.Id);

                if (content == null)
                {
                    content = await _factory.CreateAsync<DynamicContent>(contentType);
                    content.Id = model.Id;
                }

                content.TypeId = model.TypeId;
                content.Title = model.Title;
                content.Excerpt = model.Excerpt;
                content.PrimaryImage = model.PrimaryImage;

                // Save regions
                foreach (var region in contentType.Regions)
                {
                    var modelRegion = model.Regions
                        .FirstOrDefault(r => r.Meta.Id == region.Id);

                    if (region.Collection)
                    {
                        var listRegion = (IRegionList)((IDictionary<string, object>)content.Regions)[region.Id];

                        listRegion.Clear();

                        foreach (var item in modelRegion.Items)
                        {
                            if (region.Fields.Count == 1)
                            {
                                listRegion.Add(item.Fields[0].Model);
                            }
                            else
                            {
                                var postRegion = new ExpandoObject();

                                foreach (var field in region.Fields)
                                {
                                    var modelField = item.Fields
                                        .FirstOrDefault(f => f.Meta.Id == field.Id);
                                    ((IDictionary<string, object>)postRegion)[field.Id] = modelField.Model;
                                }
                                listRegion.Add(postRegion);
                            }
                        }
                    }
                    else
                    {
                        var postRegion = ((IDictionary<string, object>)content.Regions)[region.Id];

                        if (region.Fields.Count == 1)
                        {
                            ((IDictionary<string, object>)content.Regions)[region.Id] =
                                modelRegion.Items[0].Fields[0].Model;
                        }
                        else
                        {
                            foreach (var field in region.Fields)
                            {
                                var modelField = modelRegion.Items[0].Fields
                                    .FirstOrDefault(f => f.Meta.Id == field.Id);
                                ((IDictionary<string, object>)postRegion)[field.Id] = modelField.Model;
                            }
                        }
                    }
                }

                // Save content
                await _api.Content.SaveAsync(content);
            }
            else
            {
                throw new ValidationException("Invalid Content Type.");
            }
        }

        /// <summary>
        /// Deletes the content with the given id.
        /// </summary>
        /// <param name="id">The unique id</param>
        public async Task DeleteAsync(Guid id)
        {
            await _api.Content.DeleteAsync(id);
        }

        /// <summary>
        /// Transform content to a edit model.
        /// </summary>
        /// <param name="content">The dynamic content object</param>
        /// <returns>Edit model</returns>
        private ContentEditModel Transform(DynamicContent content)
        {
            var config = new Config(_api);
            var type = App.ContentTypes.GetById(content.TypeId);

            var model = new ContentEditModel
            {
                Id = content.Id,
                TypeId = content.TypeId,
                PrimaryImage = content.PrimaryImage,
                Title = content.Title,
                UsePrimaryImage = type.UsePrimaryImage,
                UseExcerpt = type.UseExcerpt,
                UseHtmlExcerpt = config.HtmlExcerpt
            };

            foreach (var regionType in type.Regions)
            {
                var region = new Models.Content.RegionModel
                {
                    Meta = new Models.Content.RegionMeta
                    {
                        Id = regionType.Id,
                        Name = regionType.Title,
                        Description = regionType.Description,
                        Placeholder = regionType.ListTitlePlaceholder,
                        IsCollection = regionType.Collection,
                        Expanded = regionType.ListExpand,
                        Icon = regionType.Icon,
                        Display = regionType.Display.ToString().ToLower()
                    }
                };
                var regionListModel = ((IDictionary<string, object>)content.Regions)[regionType.Id];

                if (!regionType.Collection)
                {
                    var regionModel = (IRegionList)Activator.CreateInstance(typeof(RegionList<>).MakeGenericType(regionListModel.GetType()));
                    regionModel.Add(regionListModel);
                    regionListModel = regionModel;
                }

                foreach (var regionModel in (IEnumerable)regionListModel)
                {
                    var regionItem = new Models.Content.RegionItemModel();

                    foreach (var fieldType in regionType.Fields)
                    {
                        var appFieldType = App.Fields.GetByType(fieldType.Type);

                        var field = new Models.Content.FieldModel
                        {
                            Meta = new Models.Content.FieldMeta
                            {
                                Id = fieldType.Id,
                                Name = fieldType.Title,
                                Component = appFieldType.Component,
                                Placeholder = fieldType.Placeholder,
                                IsHalfWidth = fieldType.Options.HasFlag(FieldOption.HalfWidth),
                                Description = fieldType.Description
                            }
                        };

                        if (typeof(SelectFieldBase).IsAssignableFrom(appFieldType.Type))
                        {
                            foreach(var item in ((SelectFieldBase)Activator.CreateInstance(appFieldType.Type)).Items)
                            {
                                field.Meta.Options.Add(Convert.ToInt32(item.Value), item.Title);
                            }
                        }

                        if (regionType.Fields.Count > 1)
                        {
                            field.Model = (IField)((IDictionary<string, object>)regionModel)[fieldType.Id];

                            if (regionType.ListTitleField == fieldType.Id)
                            {
                                regionItem.Title = field.Model.GetTitle();
                                field.Meta.NotifyChange = true;
                            }
                        }
                        else
                        {
                            field.Model = (IField)regionModel;
                            field.Meta.NotifyChange = true;
                            regionItem.Title = field.Model.GetTitle();
                        }
                        regionItem.Fields.Add(field);
                    }

                    if (string.IsNullOrWhiteSpace(regionItem.Title))
                    {
                        regionItem.Title = "...";
                    }

                    region.Items.Add(regionItem);
                }
                model.Regions.Add(region);
            }


            // Custom editors
            foreach (var editor in type.CustomEditors)
            {
                model.Editors.Add(new Models.Content.EditorModel
                {
                    Component = editor.Component,
                    Icon = editor.Icon,
                    Name = editor.Title
                });
            }
            return model;
        }
    }
}
