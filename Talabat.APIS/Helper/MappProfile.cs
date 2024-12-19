using AutoMapper;
using Talabat.APIS.DTOs.NewFolder;
using Talabat.APIS.DTOs.Order;
using Talabat.APIS.DTOs.ProductDto;
using Talabat.APIS.DTOs.Shared;
using Talabat.core.Entites;
using Talabat.core.Entitys;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.APIS.Helper
{
    public class MappProfile:Profile
    {
        public MappProfile()
        {
            CreateMap<Product, ProductReturnDto>().ForMember(p => p.ProductBrand, op => op.MapFrom(D => D.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(D => D.ProductType.Name)).ForMember(p => p.PictureUrl, Op => Op.MapFrom<ResolevePicture>());


            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Identity_Address, AddressDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Order, ResponseOrderDto>()
                .ForMember(order => order.DeliveryMethodName, Request => Request.MapFrom(na => na.DeliveryMethod.ShortName))
                .ForMember(order => order.DeliveryMethodCost, Request => Request.MapFrom(co => co.DeliveryMethod.Cost));

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dto => dto.ProductId, map => map.MapFrom(p => p.ProductItemOrdered.ProductId))
                .ForMember(dto => dto.PictureUrl, map => map.MapFrom<resolverOrder>());

        }
    }
}
