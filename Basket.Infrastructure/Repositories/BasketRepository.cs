using Basket.Application.Repositories;
using Basket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private static List<ShoppingCart> _carts = new List<ShoppingCart> { new ShoppingCart { UserName = "ravi", Items = new List<ShoppingCartItem> { new ShoppingCartItem { ImageFile = "a", Price = 10, ProductId = "G001", ProductName = "guitar", Quantity = 1 } } } };

        public async Task<ShoppingCart> GetBasket(string userName)
        {

            await Task.Delay(1000);
            return _carts.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await Task.Delay(1000);
            _carts.Add(shoppingCart);
            return _carts.FirstOrDefault(x => x.UserName == shoppingCart.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await Task.Delay(1000);
            var userCart = _carts.FirstOrDefault(x => x.UserName == userName);
            if (userCart != null)
            {
                _carts.Remove(userCart);
            }
        }
    }
}
