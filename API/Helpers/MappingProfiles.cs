using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // the way we configure automapper to map properties as brand and type name properly
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(pb => pb.ProductBrand, p => p.MapFrom(x => x.ProductBrand.Name))
            .ForMember(pt => pt.ProductType, p => p.MapFrom(x => x.ProductType.Name))
            .ForMember(pu => pu.PictureUrl, p => p.MapFrom<ProductPictureUrlResolver>());

        CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

        CreateMap<CustomerBasketDto, CustomerBasket>();

        CreateMap<BasketItemDto, BasketItem>();

        CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

        CreateMap<Order, OrderToReturnDto>()
             .ForMember(o => o.DeliveryMethod,o => o.MapFrom(s => s.DeliveryMethod.ShortName))
             .ForMember(d => d.ShippingPrice, d => d.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(p => p.ProductId, p => p.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(p => p.ProductName, p => p.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(p => p.PictureUrl, p => p.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(p => p.PictureUrl, p => p.MapFrom<OrderItemUrlResolver>());

    }
}
