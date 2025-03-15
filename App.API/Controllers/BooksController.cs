using App.Repositories.Books;
using App.Services.Books;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var booksResult = await _bookService.GetAllListAsync();

        if (booksResult.HttpStatusCode == HttpStatusCode.NoContent)
        {


            return new ObjectResult(null)
            {
                StatusCode = booksResult.HttpStatusCode.GetHashCode()
            };
        }
        return new ObjectResult(booksResult)
        {
            StatusCode = booksResult.HttpStatusCode.GetHashCode()
        };


    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var bookResult = await _bookService.GetByIdAsync(id);
        return new ObjectResult(bookResult)
        {
            StatusCode = bookResult.HttpStatusCode.GetHashCode()
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto requestDto)
    {
        var createResult = await _bookService.CreateAsync(requestDto);
        return new ObjectResult(createResult)
        {
            StatusCode = createResult.HttpStatusCode.GetHashCode()
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequestDto requestDto)
    {
        var updateResult = await _bookService.UpdateAsync(id, requestDto);
        return new ObjectResult(updateResult)
        {
            StatusCode = updateResult.HttpStatusCode.GetHashCode()
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _bookService.DeleteAsync(id);
        return new ObjectResult(deleteResult)
        {
            StatusCode = deleteResult.HttpStatusCode.GetHashCode()
        };
    }
}

