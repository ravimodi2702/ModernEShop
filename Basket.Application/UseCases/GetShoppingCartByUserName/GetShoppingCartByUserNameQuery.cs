using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.UseCases.GetShoppingCartByUserName
{
    public class GetShoppingCartByUserNameQuery : IRequest<ShoppingCartResponse>
    {
        public GetShoppingCartByUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}
