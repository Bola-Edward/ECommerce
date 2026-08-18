using ECommerce.Domain.Entities;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Types.Dtos;
using Mapster;


namespace ECommerce.UseCases.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductEntity, GetProductByIdResponse>()
                .Map(dest => dest.ProductBrand, src => src.ProductBrand.Name)
                .Map(dest => dest.ProductType, src => src.ProductType.Name);


            config.NewConfig<ProductEntity, GetAllProductsResponse>()
            .Map(dest => dest.ProductBrand, src => src.ProductBrand.Name)
            .Map(dest => dest.ProductType, src => src.ProductType.Name);


            config.NewConfig<ProductBrandEntity, GetAllBrandsResponse>();

            config.NewConfig<ProductTypeEntity, GetAllTypesResponse>();
        }
    }
}
