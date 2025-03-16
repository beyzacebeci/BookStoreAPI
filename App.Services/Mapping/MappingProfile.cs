using App.Repositories.Books;
using App.Services.Books;
using App.Services.Books.Create;
using App.Services.Books.Update;
using AutoMapper;

namespace App.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity -> DTO
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookResponseDto>();

        // DTO -> Entity
        CreateMap<CreateBookRequestDto, Book>();
        CreateMap<UpdateBookRequestDto, Book>();
    }
}

