using App.Repositories;
using App.Repositories.Categories;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();


            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                
            }).ToList();

            return ServiceResult<List<CategoryDto>>.Success(categoryDtos);

        }

    }
}
