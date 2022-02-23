using AutoMapper;
using ShopApi.Entities;
using ShopApi.Models;

namespace ShopApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<From, To>()

            CreateMap<Shop, ShopDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<CreateShopDto, Shop>()
            .ForMember(r => r.Address,
                c => c.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<UpdateShopDto, Shop>()
            .ForMember(r => r.Address,
                c => c.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<CreateUpdateProductDto, Product>()
                 .ForMember(m => m.ImageUrl, c => c.MapFrom(s => s.file.FileName));
            CreateMap<Product, ProductDto>();
                 
        }
    }
}
