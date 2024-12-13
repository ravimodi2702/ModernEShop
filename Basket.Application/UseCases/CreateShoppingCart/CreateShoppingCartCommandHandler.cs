using AutoMapper;
using Basket.Application.IHttpHandlers;
using Basket.Application.Repositories;
using Basket.Application.Responses;
using Basket.Core;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.UseCases.CreateShoppingCart
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IDiscountApiClient _discountapiClient;
        private readonly IValidator<CreateShoppingCartCommand> _validator;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IDiscountApiClient discountapiClient, IMapper mapper, IValidator<CreateShoppingCartCommand> validator)
        {
            _basketRepository = basketRepository;
            _discountapiClient = discountapiClient;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);
            foreach (var item in command.Items)
            {
                var discount = await _discountapiClient.GetDiscount(item.ProductName);
                item.Price -= discount;
            }
            var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName = command.UserName,
                Items = command.Items
            });
            return _mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
