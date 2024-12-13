using Catalog.Application.ResponseDto;
using Catalog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mappings
{
    public class CatalogMappingProfile : AutoMapper.Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
