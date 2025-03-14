namespace App.Services.Books;

public interface IBookService
{
    Task<ServiceResult<List<BookDto>>> GetAllListAsync();
    Task<ServiceResult<BookDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateBookResponseDto>> CreateAsync(CreateBookRequestDto requestDto);
    Task<ServiceResult> UpdateAsync(int id, UpdateBookRequestDto requestDto);
    Task<ServiceResult> DeleteAsync(int id);

}

