using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany_task.Data;
using OneToMany_task.Helpers.Extensions;
using OneToMany_task.Models;
using OneToMany_task.ViewModels.Categories;
using OneToMany_task.ViewModels.Sliders;

namespace OneToMany_task.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

		public SliderController(AppDbContext context,
			                    IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<Slider> sliders = await _context.Sliders.ToListAsync();

			List<SliderVM> result = sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Image }).ToList();

			
            return View(result);
		}


		[HttpGet]
        public IActionResult Create()
        {
			return View();
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
			if (!ModelState.IsValid) return View();

			foreach (var item in request.Images)
			{
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File must be only image format");
                    return View();
                }

                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Images", "Image size must be max 200kb");
                }
            }

			foreach (var item in request.Images)
			{
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                ViewBag.fileName = path;

                await item.SavFileToLocalAsync(path);

                await _context.Sliders.AddAsync(new Slider { Image = fileName });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}

