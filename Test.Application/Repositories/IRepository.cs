using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Application.Repositories
{
    public interface IRepository
    {
        Task<IList<Student>> GetData(int id);
    }
}
