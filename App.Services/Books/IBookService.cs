using App.Services.Books.Create;
using App.Services.Books.Update;

namespace App.Services.Books;

public interface IBookService
{
    Task<ServiceResult<List<BookDto>>> GetAllListAsync();
    Task<ServiceResult<BookDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateBookResponseDto>> CreateAsync(CreateBookRequestDto requestDto);
    Task<ServiceResult> UpdateAsync(int id, UpdateBookRequestDto requestDto);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<List<BookDto>>> SearchByTitleAsync(string title);
    Task<ServiceResult<List<BookDto>>> GetBooksByCategoryAsync(int id);
}

