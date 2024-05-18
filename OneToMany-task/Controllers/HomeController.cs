using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany_task.Data;
using OneToMany_task.Models;
using OneToMany_task.Services.Interfaces;
using OneToMany_task.ViewModels;

namespace OneToMany_task.Controllers
{
	public class HomeController : Controller
	{
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertService _expertService;
        private readonly IExpertImageService _expertImageService;

        public HomeController(AppDbContext context,
                              IProductService productService,
                              ICategoryService categoryService,
                              IExpertService expertService,
                              IExpertImageService expertImageService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _expertService = expertService;
            _expertImageService = expertImageService;
        }

        public async Task <IActionResult> Index()
        {
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Blog> blogs = await _context.Blogs.Where(m=>!m.SoftDeleted).Take(3).ToListAsync();
            List<Expert> experts = await _expertService.GetAllAsync();
            List<ExpertImage> expertImageService= await _expertImageService.GetAllAsync();


            HomeVM model = new()
            {
                Categories = categories,
                Blogs = blogs,
                Products = products,
                Experts = experts,
                ExpertImages = expertImageService
            };

            return View(model);
        }
    }
}

