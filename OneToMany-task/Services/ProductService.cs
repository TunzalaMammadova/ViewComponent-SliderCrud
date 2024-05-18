using System;
using Microsoft.EntityFrameworkCore;
using OneToMany_task.Data;
using OneToMany_task.Models;
using OneToMany_task.Services.Interfaces;

namespace OneToMany_task.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.ProductImage).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImage)
                                          .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

