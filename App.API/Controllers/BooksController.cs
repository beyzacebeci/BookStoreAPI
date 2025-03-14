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


}

