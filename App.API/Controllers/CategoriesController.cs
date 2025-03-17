using App.Services.Categories;
using App.Services.Categories.Create;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categoriesResult = await _categoryService.GetAllListAsync();

        if (categoriesResult.HttpStatusCode == HttpStatusCode.NoContent)
        {


            return new ObjectResult(null)
            {
                StatusCode = categoriesResult.HttpStatusCode.GetHashCode()
            };
        }
        return new ObjectResult(categoriesResult)
        {
            StatusCode = categoriesResult.HttpStatusCode.GetHashCode()
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto requestDto)
    {
        var createResult = await _categoryService.CreateAsync(requestDto);
        return new ObjectResult(createResult)
        {
            StatusCode = createResult.HttpStatusCode.GetHashCode()
        };
    }
}

