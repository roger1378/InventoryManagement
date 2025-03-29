using InventoryManagement.Service.Products;
using InventoryManagement.Service.Products.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Orders.Api.Controllers.Products;

/// <summary>
/// Product controller with authentication.
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;

    }

    /// <summary>Returns all products.</summary>
    /// <returns>Returns all products.</returns>
    /// <response code ="200">Returns data for products page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _mediator.Send(new GetProductsQuery());

        return Ok(result);
    }

    /// <summary>Returns product by id.</summary>
    /// <returns>Returns product by id.</returns>
    /// <response code ="200">Returns data for products page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        return Ok(result);
    }

    /// <summary>Insert products.</summary>
    /// <returns>Status code.</returns>
    /// <param name="ProductCreateRequestDto">Product information.</param>
    /// <response code ="200">Returns Success.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost()]
    public async Task<IActionResult> InsertAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _mediator.Send(new InsertProductCommand(productCreateRequestDto));

        return Created("Created", result);
    }

    /// <summary>Update products.</summary>
    /// <returns>Status code.</returns>
    /// <param name="ProductUpdateRequestDto">Product information.</param>
    /// <response code ="200">Returns Success.</response>
    /// <response code ="404">Returns Product was not found.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>WProducts</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPut()]
    public async Task<IActionResult> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto)
    {
        var result = await _mediator.Send(new UpdateProductCommand(productUpdateRequestDto));
        
        return Ok(result);
    }

    /// <summary>Delete an existing product.</summary>
    /// <param name="id">Product ID.</param>
    /// <response code ="200">Returns OK.</response>
    /// <response code ="404">Returns Product was not found.</response>
    /// <response code ="500">Returns a server error.</response>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheme([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));

        return Ok();
    }
}
