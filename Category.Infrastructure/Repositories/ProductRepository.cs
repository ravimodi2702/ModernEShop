using Catalog.Application.Repositories;
using Catalog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public static List<Product> products = new List<Product>();
        public async Task<IList<Product>> GetProductsByCategoryAsync(string category)
        {
            await Task.Delay(1);
            return products.Where(x => x.Category.Name == category).ToList();
        }

        public async Task<IList<Product>> GetProudctsByCategoryIdAsync(int categoryId)
        {
            await Task.Delay(1);
            return products.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
