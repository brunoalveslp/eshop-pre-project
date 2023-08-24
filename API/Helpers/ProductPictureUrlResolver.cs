using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;
// configuring to images be located on right place in the project so we can use images properly
public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    // IConfiguration from Microsoft not from Mapper
    private readonly IConfiguration _config;
    public ProductPictureUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public IConfiguration Config { get; }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if(!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }

        return null;
    }
}
