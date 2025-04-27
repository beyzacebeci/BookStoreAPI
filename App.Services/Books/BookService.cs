using App.Repositories;
using App.Repositories.Books;
using App.Repositories.Categories;
using App.Services.Books.Create;
using App.Services.Books.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Books;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<BookDto>>> GetAllListAsync()
    {
        var books = await _bookRepository.GetAll().ToListAsync();

        var booksDto = _mapper.Map<List<BookDto>>(books);

        return ServiceResult<List<BookDto>>.Success(booksDto);

    }

    public async Task<ServiceResult<BookDto?>> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return ServiceResult<BookDto?>.Fail("Book is not found.", HttpStatusCode.NotFound);
        }

        var bookDto = _mapper.Map<BookDto>(book);

        return ServiceResult<BookDto>.Success(bookDto)!;
    }
    public async Task<ServiceResult<CreateBookResponseDto>> CreateAsync(CreateBookRequestDto requestDto)
    {
        var category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId);
        if (category is null)
        {
            return ServiceResult<CreateBookResponseDto>.Fail(
                "Category does not exist.",
                HttpStatusCode.NotFound);
        }

        var anybook = await _bookRepository
            .Where(x => x.Title == requestDto.Title)
            .AnyAsync();

        if (anybook)
        {
            return ServiceResult<CreateBookResponseDto>.Fail(
                "A book with the same title and author already exists.",
                HttpStatusCode.BadRequest);
        }

        var book = _mapper.Map<Book>(requestDto);

        await _bookRepository.AddAsync(book);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateBookResponseDto>.Success(new CreateBookResponseDto { Id = book.Id }, HttpStatusCode.Created);
    }

    public async Task<ServiceResult<List<BookDto>>> SearchByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<List<BookDto>>.Fail(
                "Search text cannot be empty.",
                HttpStatusCode.BadRequest);
        }

        var books = await _bookRepository.SearchByTitleAsync(title);

        if (!books.Any())
        {
            return ServiceResult<List<BookDto>>.Success(
                new List<BookDto>(),
                HttpStatusCode.NoContent);
        }

        var booksDto = _mapper.Map<List<BookDto>>(books);

        return ServiceResult<List<BookDto>>.Success(booksDto);
    }

    public async Task<ServiceResult<List<BookDto>>> GetBooksByCategoryAsync(int id)
    {
        var categoryExists = await _categoryRepository.GetByIdAsync(id);

        if (categoryExists is null)
        {
            return ServiceResult<List<BookDto>>.Fail(
                "Category does not exist.",
                HttpStatusCode.NotFound);
        }

        var books = await _bookRepository.GetBooksByCategoryAsync(id);

        var booksDto = _mapper.Map<List<BookDto>>(books);

        return ServiceResult<List<BookDto>>.Success(booksDto)!;

    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateBookRequestDto requestDto)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return ServiceResult.Fail("Book not found.", HttpStatusCode.NotFound);
        }

        var category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId);
        if (category is null)
        {
            return ServiceResult.Fail("Category does not exist.", HttpStatusCode.NotFound);
        }

        _mapper.Map(requestDto, book);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return ServiceResult.Fail("Book not found.", HttpStatusCode.NotFound);
        }

        book.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}


