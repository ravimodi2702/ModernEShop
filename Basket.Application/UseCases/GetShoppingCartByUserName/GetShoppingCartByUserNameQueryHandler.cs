using AutoMapper;
using Basket.Application.Repositories;
using Basket.Application.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Application.UseCases.GetShoppingCartByUserName
{
    public class GetShoppingCartByUserNameQueryHandler : IRequestHandler<GetShoppingCartByUserNameQuery, ShoppingCartResponse>
    {
        private IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartByUserNameQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartResponse> Handle(GetShoppingCartByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.GetBasket(request.UserName);
            return _mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
