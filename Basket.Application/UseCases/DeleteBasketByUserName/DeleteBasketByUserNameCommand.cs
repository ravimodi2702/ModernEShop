using Basket.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.UseCases.DeleteBasketByUserName
{
    public class DeleteBasketByUserNameCommand : IRequest<bool>
    {
        public DeleteBasketByUserNameCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}
