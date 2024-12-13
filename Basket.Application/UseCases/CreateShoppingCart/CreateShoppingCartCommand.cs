using Basket.Application.Responses;
using Basket.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.UseCases.CreateShoppingCart
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
