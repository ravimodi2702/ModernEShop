using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Repositories;
using Test.Core;

namespace Test.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        public async Task<IList<Student>> GetData(int id)
        {
            await Task.Delay(1);
            return new List<Student>() { new Student { Id = 1, Name = "Ravi" } };
        }
    }
}
