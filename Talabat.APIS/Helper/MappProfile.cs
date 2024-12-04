using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.core.Entites;

namespace Talabat.APIS.Helper
{
    public class MappProfile:Profile
    {
        public MappProfile()
        {
            CreateMap<Product, ProductReturnDto>().ForMember(p => p.ProductBrand, op => op.MapFrom(D => D.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(D => D.ProductType.Name)).ForMember(p => p.PictureUrl, Op => Op.MapFrom<ResolevePicture>());


        }
    }
}
