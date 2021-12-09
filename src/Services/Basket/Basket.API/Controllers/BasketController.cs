using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("Api/v1/[controller]")]
    public class BasketController : ControllerBase 
    {

        private IBasketRepository _IBasketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
           this. _IBasketRepository = basketRepository;
        }

        [HttpGet("{username}",Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart ),(int)HttpStatusCode.OK) ]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _IBasketRepository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpateBasket([FromBody]ShoppingCart basket)
        {
            var resut = await _IBasketRepository.UpdateBasket(basket);
            return Ok(resut);
        }


        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string username)
        {
             await _IBasketRepository.DeleteBasket(username);
            return Ok();
        }
    }

   
}
