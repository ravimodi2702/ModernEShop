using Catalog.Application.ResponseDto;
using Catalog.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.UseCases.GetCategoriesById
{
    public class GetCategoriesByIdQuery : IRequest<CategoryDto>
    {
        public int CategoryId { get; set; }
        public GetCategoriesByIdQuery(int categoryId)
        {
            this.CategoryId = categoryId;
        }
    }
}
