using AutoMapper;
using Talabat.APIS.DTOs.NewFolder;
using Talabat.core.Entitys.Order_Aggregate;

public class resolverOrder : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration;

    public resolverOrder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        return source?.ProductItemOrdered?.PictureUrl == null
            ? string.Empty
            : $"{_configuration["ApiPictureResolver"]}/{source.ProductItemOrdered.PictureUrl}";
    }
}
