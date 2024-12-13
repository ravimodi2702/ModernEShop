using Catalog.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Application.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetProductsByCategoryAsync(string category);
        Task<IList<Product>> GetProudctsByCategoryIdAsync(int categoryId);
    }
}
