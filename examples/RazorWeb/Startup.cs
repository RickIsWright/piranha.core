﻿/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/tidyui/coreweb
 *
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha;
using Piranha.Data.EF.SQLite;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AttributeBuilder;
using Piranha.Local;
using RazorWeb.Models;
using Piranha.Data.EF.SQLServer;
using Piranha.AspNetCore.Identity.SQLServer;
using Microsoft.Extensions.Configuration;

namespace RazorWeb
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPiranha(options =>
            {
                options.AddRazorRuntimeCompilation = true; // Disable for Production use. Set to false.

                options.UseFileStorage(naming: FileStorageNaming.UniqueFolderNames);
                options.UseImageSharp();
                options.UseManager();
                options.UseTinyMCE();
                options.UseMemoryCache();
                // Other options for headless
                // Convenience method for disabling all Piranha middleware components. 
                // options.DisableRouting(); // Disables All routing features of Piranha default is enabled
                // options.UseAliasRouting = true; // default is true.
                // options.UseArchiveRouting = false; // default is true.
                // options.UsePageRouting = true; // default is true
                // options.UsePostRouting = true; // default is true
                // options.UseSitemapRouting = true; // default is true.MiddleWare listens for request to /Sitemap.xml
                // options.UseSiteRouting = true; // Enable site routing. can be disabled if only one site exists.
                // options.UseStartpageRouting = true; // enabled in middleware. default is true

                string conStringSf = Configuration.GetConnectionString("piranhasf");
                // sql server
                //options.UseEF<SQLServerDb>(db =>
                //    db.UseSqlServer(Configuration.GetConnectionString("piranhasf")));

                //options.UseIdentityWithSeed<IdentitySQLServerDb>(db =>
                //db.UseSqlServer(Configuration.GetConnectionString("piranhasf")));


                options.UseEF<SQLiteDb>(db =>
                    db.UseSqlite("Filename=./piranha.razorweb.db"));
                options.UseIdentityWithSeed<IdentitySQLiteDb>(db =>
                    db.UseSqlite("Filename=./piranha.razorweb.db"));

                options.UseSecurity(o =>
                {
                    o.UsePermission("Subscriber");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            App.Init(api); // Piranha.App.Init() Method

            // Configure cache level
            App.CacheLevel = Piranha.Cache.CacheLevel.Basic; //using none while developing ;

            // Register custom components
            App.Blocks.Register<RazorWeb.Models.Blocks.MyGenericBlock>();
            App.Blocks.Register<RazorWeb.Models.Blocks.RawHtmlBlock>();
            App.Modules.Manager().Scripts.Add("~/assets/custom-blocks.js");
            App.Modules.Manager().Styles.Add("~/assets/custom-blocks.css");

            // Build content types
            new ContentTypeBuilder(api)
                .AddAssembly(typeof(Startup).Assembly) // ads all content types
               // .AddType(typeof(SimplePage))
                .Build()
                .DeleteOrphans(); // not deleting orphans yet.

            // Configure editor
            Piranha.Manager.Editor.EditorConfig.FromFile("editorconfig.json");

            /**
             *
             * Test another culture in the UI
             *
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
             */

            app.UsePiranha(options =>
            {
                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity(); 
            });

            // Seed.RunAsync(api).GetAwaiter().GetResult();
        }
    }
}
