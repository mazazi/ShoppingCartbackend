using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using Tatweer.Application.Commands.Products;
using Tatweer.Application.Models;
using Tatweer.Application.Responses.Cart;
using Tatweer.Application.Responses.Products;
using Tatweer.Application.Services;

namespace Tatweer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IShoppingCartQuery _cartQuery;
        public ShoppingCartController(IMediator mediator, IShoppingCartQuery cartQuery) : base()
        {
            _mediator = mediator;
            _cartQuery = cartQuery;
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="command">The command to add a Cart Items.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [Route("CheckOut")]
        [HttpPost]
        public async Task<Result> CheckOut([FromBody] SaveShoppingCartCommand saveProductCommand)
        {
            var result = await _mediator.Send(saveProductCommand);
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

      
        /// <summary>
        /// Gets a list of  shopping cart.
        /// </summary>
        /// <param name="page">The number of page should be displayed.</param>
        /// <param name="pageSize">The number of pageSize will be returns.</param>
        /// <returns>A list of Cart Items.</returns>

        [HttpGet]
        [Route("GetAllWithPager")]
        public IActionResult GetAllWithPager(int page, int pageSize)
        {
            int total = 0;
            return Ok(_cartQuery.GetAllWithPager(page, pageSize, out total));
        }

    }
}
