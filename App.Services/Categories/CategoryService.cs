using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;

namespace App.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<ServiceResult<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto requestDto)
        {
            var anyCategory = await _categoryRepository
                .Where(x => x.Name == requestDto.Name)
                .AnyAsync();

            if (anyCategory)
            {
                return ServiceResult<CreateCategoryResponseDto>.Fail(
                    "A category with the same name already exists.",
                    HttpStatusCode.BadRequest);
            }

            var category = _mapper.Map<Category>(requestDto);

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateCategoryResponseDto>.Success(
                new CreateCategoryResponseDto { Id = category.Id },
                HttpStatusCode.Created);
        }
    }
}
