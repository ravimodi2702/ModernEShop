using Catalog.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public static List<Core.Category> categories = new List<Core.Category>
        {
            new Core.Category{ Id = 1, Name = "Cat1" }
        };
        public async Task<IList<Core.Category>> GetAllCategoriesAsync()
        {
            await Task.Delay(1);
            return categories.ToList();
        }

        public async Task<Core.Category> GetCategoryByIdAsync(int id)
        {
            await Task.Delay(1);
            return categories.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Core.Category> GetCategoryByNameAsync(string name)
        {
            await Task.Delay(1);
            return categories.FirstOrDefault(x => x.Name == name);
        }

        public async Task AddCategoryAsync(Core.Category category)
        {
            await Task.Delay(1);
            categories.Add(category);
        }
    }
}
