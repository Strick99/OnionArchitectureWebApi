using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a New Product.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductCommandDto request)
        {
            return Ok(await _mediator.Send(new CreateProductCommand(request.Name,
                                                                   request.Barcode,
                                                                   request.IsActive,
                                                                   request.Description,
                                                                   request.BuyingPrice,
                                                                   request.Rate)));
        }

        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        /// <summary>
        /// Gets Product Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductByIdCommandDto { Id = id }));
        }

        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommandDto command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}