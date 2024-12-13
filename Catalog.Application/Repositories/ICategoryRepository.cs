using Catalog.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Core.Category category);
    }
}
