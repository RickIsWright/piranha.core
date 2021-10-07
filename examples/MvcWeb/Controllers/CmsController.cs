/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/piranhacms/piranha.core
 *
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Piranha.Models;
using MvcWeb.Models;

namespace MvcWeb.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly IDb _db;
        private readonly IModelLoader _loader;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="app">The current app</param>
        public CmsController(IApi api, IDb db, IModelLoader loader)
        {
            _api = api;
            _db = db;
            _loader = loader;
        }

        /// <summary>
        /// Gets the blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        [HttpGet]
        [Route("archive")]
        public async Task<IActionResult> Archive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null)
        {
            var model = await _api.Pages.GetByIdAsync<Models.BlogArchive>(id);
            model.Archive = await _api.Archives.GetByIdAsync(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        [HttpGet]
        [Route("page")]
        public async Task<IActionResult> Page(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<Models.StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        [HttpGet]
        [Route("pagewide")]
        public async Task<IActionResult> PageWide(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<Models.StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the post with the given id.
        /// </summary>
        /// <param name="id">The unique post id</param>
        ///
        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> Post(Guid id, bool draft = false)
        {
            var model = await _loader.GetPostAsync<Models.BlogPost>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the teaser page with the given id.
        /// </summary>
        /// <param name="id">The page id</param>
        /// <param name="startpage">If this is the startpage of the site</param>
        [HttpGet]
        [Route("teaserpage")]
        public async Task<IActionResult> TeaserPage(Guid id, bool startpage = false, bool draft = false)
        {
            var model = await _loader.GetPageAsync<Models.TeaserPage>(id, HttpContext.User, draft);

            if (startpage)
            {
                var latest = _db.Posts
                    .Where(p => p.Published <= DateTime.Now)
                    .OrderByDescending(p => p.Published)
                    .Take(1)
                    .Select(p => p.Id);
                if (latest.Count() > 0)
                {
                    model.LatestPost = await _api.Posts
                        .GetByIdAsync<PostInfo>(latest.First());
                }
                return View("startpage", model);
            }
            return View(model);
        }

        [Route("catalog")]
        public async Task<IActionResult> Catalog(Guid id)
        {
            var catalog = await _api.Pages.GetByIdAsync<CatalogPage>(id);

            var model = new CatalogViewModel
            {
                CatalogPage = catalog,
                Categories = (await _api.Sites.GetSitemapAsync())
                // get the catalog page
                .Where(item => item.Id == catalog.Id)
                // get its children
                .SelectMany(item => item.Items)
                // for each child sitemap item, get the page
                // and return a simplified model for the view
                .Select(item =>
                {
                    var page = _api.Pages.GetByIdAsync<CategoryPage>
                (item.Id).Result;

                    var ci = new CategoryItem
                    {
                        Title = page.Title,
                        Description = page.CategoryDetail.Description,
                        PageUrl = page.Permalink,
                        ImageUrl = page.CategoryDetail.CategoryImage
                  .Resize(_api, 200)
                    };
                    return ci;
                })
            };
            return View(model);
        }



        [Route("resource-catalog")]
        // [Route("faq")]
        public async Task<IActionResult> ResourceCatalog(Guid id)
        {
            var catalog = await _api.Pages.GetByIdAsync<ResourceCatalogPage>(id);

            var model = new ResourceCatalogViewModel
            {
                ResourceCatalogPage = catalog,
                ResourceCategories = (await _api.Sites.GetSitemapAsync())
                // get the catalog page
                .Where(item => item.Id == catalog.Id)
                // get its children
                .SelectMany(item => item.Items)
                // for each child sitemap item, get the page
                // and return a simplified model for the view
                .Select(item =>
                {
                    var page = _api.Pages.GetByIdAsync<ResourceCategoryPage>
                (item.Id).Result;

                    var ci = new ResourceCategoryItem
                    {
                        Title = page.Title,
                        Description = page.ResourceCategoryDetail.Description,
                        PageUrl = page.Permalink,
                        ImageUrl = page.ResourceCategoryDetail.CategoryImage
                  .Resize(_api, 200)
                    };
                    return ci;
                })
            };
            return View(model);
        }

        // faq
        [Route("/faq")]
        public async Task<IActionResult> Faq(Guid id)
        {
            var catalog = await _api.Pages.GetByIdAsync<FaqPage>(id);

            var model = new MvcWeb.Models.FaqViewModel
            {
                FaqPage = catalog,
                ResourceCategories = (await _api.Sites.GetSitemapAsync())
                // get the catalog page
                .Where(item => item.Id == catalog.Id)
                // get its children
                .SelectMany(item => item.Items)
                // for each child sitemap item, get the page
                // and return a simplified model for the view
                .Select(item =>
                {
                    var page = _api.Pages.GetByIdAsync<ResourceCategoryPage>
                (item.Id).Result;

                    var ci = new MvcWeb.Models.ResourceCategoryItem
                    {
                        Title = page.Title,
                        Description = page.ResourceCategoryDetail.Description,
                        PageUrl = page.Permalink,
                        ImageUrl = page.ResourceCategoryDetail.CategoryImage
                  .Resize(_api, 200)
                    };
                    return ci;
                })
            };

            return View(model);
        }


        [Route("catalog-category")]
        public async Task<IActionResult> Category(Guid id)
        {
            var model = await _api.Pages
              .GetByIdAsync<Models.CategoryPage>(id);
            return View(model);
        }



        [Route("resource-catalog-resource-category")]
        [Route("faq-resource-category")]
        public async Task<IActionResult> ResourceCategory(Guid id)
        {
            var model = await _api.Pages
              .GetByIdAsync<Models.ResourceCategoryPage>(id);
            return View(model);
        }


        [Route("video-catalog")]
        // [Route("faq")]
        public async Task<IActionResult> VideoCatalog(Guid id)
        {
            var catalog = await _api.Pages.GetByIdAsync<VideoCatalogPage>(id);

            var model = new VideoCatalogViewModel
            {
                VideoCatalogPage = catalog,
                VideoItems = (await _api.Sites.GetSitemapAsync())
                // get the catalog page
                .Where(item => item.Id == catalog.Id)
                // get its children
                .SelectMany(item => item.Items)
                // for each child sitemap item, get the page
                // and return a simplified model for the view
                .Select(item =>
                {
                    var page = _api.Pages.GetByIdAsync<VideoCategoryPage>
                (item.Id).Result;

                    var ci = new VideoItem
                    {
                        Title = page.Title,
                        Description = page.CategoryDetail.Description,
                        PageUrl = page.Permalink,
                        ImageUrl = page.CategoryDetail.VideoImage
                  .Resize(_api, 200)
                    };
                    return ci;
                })
            };
            return View(model);
        }
    }
}
