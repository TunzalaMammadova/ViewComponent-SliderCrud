using System;
using OneToMany_task.Models;

namespace OneToMany_task.Services.Interfaces
{
	public interface IProductService
	{
		Task<List<Product>> GetAllAsync();
		Task<Product> GetByIdAsync(int id);
	}
}

