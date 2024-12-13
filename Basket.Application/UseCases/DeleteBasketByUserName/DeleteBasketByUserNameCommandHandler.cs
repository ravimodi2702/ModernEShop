using Basket.Application.Repositories;
using Basket.Application.Responses;
using Basket.Application.UseCases.CreateShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.UseCases.DeleteBasketByUserName
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<bool> Handle(DeleteBasketByUserNameCommand command, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasket(command.UserName);
            return true;
        }
    }
}
