using System.Threading.Tasks;

namespace Basket.Application.IHttpHandlers
{
    public interface IDiscountApiClient
    {
        Task<int> GetDiscount(string productName);
    }
}
