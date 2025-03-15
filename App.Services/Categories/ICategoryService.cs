using App.Services.Books;

namespace App.Services.Categories;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();

}

