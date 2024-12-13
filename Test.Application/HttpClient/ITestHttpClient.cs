using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Application.HttpClient
{
    public interface ITestHttpClient
    {
        Task<Student> GetAllStudent();
    }
}
