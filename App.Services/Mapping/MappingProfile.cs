using App.Repositories.Books;
using App.Repositories.Categories;
using App.Repositories.OrderItems;
using App.Repositories.Orders;
using App.Services.Books;
using App.Services.Books.Create;
using App.Services.Books.Update;
using App.Services.Categories.Create;
using App.Services.Categories;
using App.Services.Orders;
using App.Services.Orders.Create;
using App.Services.Orders.CreateOrderItem;
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

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryRequestDto, Category>();
        CreateMap<Category, CreateCategoryResponseDto>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.BookTitle,
                opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.BookAuthor,
                opt => opt.MapFrom(src => src.Book.Author))
            .ForMember(dest => dest.SubTotal,
                opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));

        CreateMap<Order, CreateOrderResponseDto>();
    }
}

