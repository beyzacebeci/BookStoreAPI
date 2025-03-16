using App.Repositories;
using App.Repositories.Books;
using App.Services.Books.Create;
using App.Services.Books.Update;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Books;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<List<BookDto>>> GetAllListAsync()
    {
        var books = await _bookRepository.GetAll().ToListAsync();

        var bookDtos = books.Select(book => new BookDto
        {
            Id = book.Id,
            CategoryId = book.CategoryId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublicationYear = book.PublicationYear
        }).ToList();

        return ServiceResult<List<BookDto>>.Success(bookDtos);

    }

    public async Task<ServiceResult<BookDto?>> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return ServiceResult<BookDto?>.Fail("Book is not found.", HttpStatusCode.NotFound);
        }

        var bookDto = new BookDto
        {
            Id = book!.Id,
            CategoryId = book.CategoryId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublicationYear = book.PublicationYear
        };

        return ServiceResult<BookDto>.Success(bookDto)!;
    }
    public async Task<ServiceResult<CreateBookResponseDto>> CreateAsync(CreateBookRequestDto requestDto)
    {
        var anybook = await _bookRepository
            .Where(x => x.Title == requestDto.Title && x.Author == requestDto.Author)
            .AnyAsync();

        if (anybook)
        {
            return ServiceResult<CreateBookResponseDto>.Fail("A book with the same title and author already exists.", HttpStatusCode.BadRequest);
        }

        var book = new Book()
        {
            CategoryId = requestDto.CategoryId,
            Title = requestDto.Title,
            Author = requestDto.Author,
            ISBN = requestDto.ISBN,
            Price = requestDto.Price,
            StockQuantity = requestDto.StockQuantity,
            PublicationYear = requestDto.PublicationYear
        };
        await _bookRepository.AddAsync(book);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateBookResponseDto>.Success(new CreateBookResponseDto { Id = book.Id });
    }

    public async Task<ServiceResult<List<BookDto>>> SearchByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<List<BookDto>>.Fail(
                "Arama metni boş olamaz.",
                HttpStatusCode.BadRequest);
        }

        var books = await _bookRepository.SearchByTitleAsync(title);

        if (!books.Any())
        {
            return ServiceResult<List<BookDto>>.Success(
                new List<BookDto>(),
                HttpStatusCode.NoContent);
        }

        var bookDtos = books.Select(book => new BookDto
        {
            Id = book.Id,
            CategoryId = book.CategoryId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublicationYear = book.PublicationYear
        }).ToList();

        return ServiceResult<List<BookDto>>.Success(bookDtos);
    }

    public async Task<ServiceResult<List<BookDto>>> GetBooksByCategoryAsync(int id)
    {
        var categoryExists = await _bookRepository.GetAll()
    .AnyAsync(b => b.CategoryId == id);

        if (!categoryExists)
        {
            return ServiceResult<List<BookDto>>.Fail(
                "Category does not exist.",
                HttpStatusCode.NotFound);
        }
        var books = await _bookRepository.GetBooksByCategoryAsync(id);

        var bookDtos = books.Select(book => new BookDto
        {
            Id = book.Id,
            CategoryId = book.CategoryId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublicationYear = book.PublicationYear
        }).ToList();

        return ServiceResult<List<BookDto>>.Success(bookDtos)!;

    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateBookRequestDto requestDto)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return ServiceResult.Fail("Book not found.", HttpStatusCode.NotFound);
        }

        book.CategoryId = requestDto.CategoryId;
        book.Title = requestDto.Title;
        book.Author = requestDto.Author;
        book.ISBN = requestDto.ISBN;
        book.Price = requestDto.Price;
        book.StockQuantity = requestDto.StockQuantity;
        book.PublicationYear = requestDto.PublicationYear;

        return ServiceResult.Success();

    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return ServiceResult.Fail("Book not found.", HttpStatusCode.NotFound);
        }
        _bookRepository.Delete(book);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}


