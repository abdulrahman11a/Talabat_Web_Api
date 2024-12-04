using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIS.DTOs;
using Talabat.core.Entites;

namespace Talabat.APIS.Helper
{
    public class ResolevePicture (IConfiguration configuration) : IValueResolver<Product, ProductReturnDto, string>
    {

        public string Resolve(Product source, ProductReturnDto destination, string destMember, ResolutionContext context)
        {
            return $"{configuration["ApiPictureResolver"]}/{source.PictureUrl}";
        }
    }
}
