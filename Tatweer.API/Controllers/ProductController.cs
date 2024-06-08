using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using Tatweer.Application.Commands.Products;
using Tatweer.Application.Models;
using Tatweer.Application.Responses.Products;
using Tatweer.Application.Services;

namespace Tatweer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IProductQuery _productQuery;
        public ProductController(IMediator mediator, IProductQuery employeeQuery) : base()
        {
            _mediator = mediator;
            _productQuery = employeeQuery;
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="command">The command to add a product.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [Route("Save")]
        [HttpPost]
        public async Task<Result> Create([FromBody] SaveProductCommand saveProductCommand)
        {
            var result = await _mediator.Send(saveProductCommand);
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

        /// <summary>
        /// modify an exist product.
        /// </summary>
        /// <param name="command">The command to update a product.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [Route("{id}/Edit")]
        [HttpPost]
        public async Task<Result> Edit(int id, [FromBody] SaveProductCommand saveProductCommand)
        {
            saveProductCommand.Id = id;
            var result = await _mediator.Send(saveProductCommand);
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

        /// <summary>
        /// Gets a list of products for the admin page.
        /// </summary>
        /// <returns>A list of products.</returns>
        /// 
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ProductsDto>>> GetAll()
        {
            return Ok(await _productQuery.GetAllAsync());
        }

        /// <summary>
        /// Gets a list of products to be displayed in shopping cart for the specified number of pages as sets in appsetting = 10 by default.
        /// </summary>
        /// <param name="name">The name of product for filter.</param>
        /// <param name="page">The number of page should be displayed.</param>
        /// <param name="pageSize">The number of pageSize will be returns.</param>
        /// <returns>A list of products.</returns>
        /// 
        [HttpGet]
        [Route("GetAllWithPager")]
        public IActionResult GetAllWithPager(int page, int pageSize, string? name = null)
        {
            int total = 0;
            return Ok(_productQuery.GetAllWithPager(page, pageSize, out total, name));
        }

        /// <summary>
        /// Gets the product for the specified product id.
        /// </summary>
        /// <param name="id">The id of product.</param>
        /// <returns>A product with id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductsDto), (int)HttpStatusCode.OK)]
        [Route("{id:int}/Get")]
        public async Task<ActionResult<ProductsDto>> GetById(int id)
        {
            var user = await _productQuery.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// make an exist product to be not visible.
        /// </summary>
        /// <param name="id">The id to update a product to be not visible.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [Route("{id}/DeActivate")]
        [HttpPost]
        public async Task<Result> MarkAsNotActive(int id)
        {
            var result = await _mediator.Send(new MarkProductAsNotVisibleCommand(id));
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }

        /// <summary>
        /// make an exist product to be visible.
        /// </summary>
        /// <param name="id">The id to update a product to be visible.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [Route("{id}/Activate")]
        [HttpPost]
        public async Task<Result> MarkAsActive(int id)
        {
            var result = await _mediator.Send(new MarkProductAsVisibleCommand(id));
            if (result.Succeeded)
                return result;

            return base.Problem(result.Errors[0]);
        }
    }
}
