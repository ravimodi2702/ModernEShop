using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.HttpClients
{
    public interface IMockApiClient
    {
        Task<MockAPiResponse> GetData(string param);
        Task<MockPostResponse> PostData(MockReq mockReq);
    }
}
