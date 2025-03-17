using App.Services.Books;
using App.Services.Categories.Create;

namespace App.Services.Categories;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
    Task<ServiceResult<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto requestDto);

}

