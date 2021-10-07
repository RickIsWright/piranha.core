using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Models;
using Piranha.AspNetCore.Services;
using Piranha.Models;
using RazorWeb.Models;



namespace RazorWeb.Pages
{
    public class HeroPageModel : SinglePage<HeroPage>
    {

        private readonly IDb _db;

        public HeroPageModel(IApi api, IModelLoader loader, IDb db) : base(api, loader)
        {
            _db = db;
        }

        public override async Task<IActionResult> OnGet(Guid id, bool draft = false)
        {
            var result = await base.OnGet(id, draft);
            
            if (Data != null && Data.IsStartPage)
            {
                var latest = await _db.Posts
                    .Where(p => p.Published <= DateTime.Now)
                    .OrderByDescending(p => p.Published)
                    .Take(1)
                    .Select(p => p.Id)
                    .ToListAsync();

                if (latest.Count() > 0)
                {
                    Data.LatestPost = await _api.Posts
                        .GetByIdAsync<PostInfo>(latest.First());
                }
            }

            if ( Data != null && Data.Images !=null && Data.Images.Count>0)
            {
                var img = Data.Images[0].Media;
                Data.Hero.Images = Data.Images;
                

            }
            return  result;
        }
        //public void OnGet()
        //{
        //}
    }
}
