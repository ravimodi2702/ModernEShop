using Basket.Application.Responses;
using Basket.Application.UseCases.CreateShoppingCart;
using Basket.Application.UseCases.DeleteBasketByUserName;
using Basket.Application.UseCases.GetShoppingCartByUserName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketController> _logger;
        // private readonly ICorrelationIdGenerator _correlationIdGenerator;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var basket = await _mediator.Send(new GetShoppingCartByUserNameQuery(userName));
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [Authorize(Policy = "canCreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {

            var basket = await _mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            return Ok(await _mediator.Send(command));
        }

        /* [Route("[action]")]
         [HttpPost]
         [ProducesResponseType((int)HttpStatusCode.Accepted)]
         [ProducesResponseType((int)HttpStatusCode.BadRequest)]
         public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
         {
             //Get existing basket with username
             var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
             var basket = await _mediator.Send(query);
             if (basket == null)
             {
                 return BadRequest();
             }

             var eventMesg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
             eventMesg.TotalPrice = basket.TotalPrice;
             eventMesg.CorrelationId = _correlationIdGenerator.Get();
             await _publishEndpoint.Publish(eventMesg);
             //remove the basket
             var deleteQuery = new DeleteBasketByUserNameQuery(basketCheckout.UserName);
             await _mediator.Send(deleteQuery);
             return Accepted();
         }*/
    }
}
