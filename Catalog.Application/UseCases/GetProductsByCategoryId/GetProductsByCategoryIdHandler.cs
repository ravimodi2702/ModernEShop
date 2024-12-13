using Catalog.Application.Repositories;
using Catalog.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdHandler : IRequestHandler<GetProductsByCategoryIdQuery, IList<Product>>
    {
        private IProductRepository _productRepository;

        public GetProductsByCategoryIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<Product>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProudctsByCategoryIdAsync(request.categoryId);
        }
    }
}
