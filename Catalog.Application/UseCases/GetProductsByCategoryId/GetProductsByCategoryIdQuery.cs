using Catalog.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.UseCases.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQuery : IRequest<IList<Product>>
    {
        public int categoryId { get; set; }
        public GetProductsByCategoryIdQuery(int categoryId)
        {
            this.categoryId = categoryId;
        }
    }
}
