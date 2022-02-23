using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using ShopApi.Services;
using System.Collections.Generic;

namespace ShopApi.Controllers
{
    [Route("/api/shop/{shopId}/product")]
    [ApiController]
    [Authorize]
    [FormatFilter]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService )
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int shopId, [FromBody] CreateUpdateProductDto dto)
        {
            var ProductId = _productService.Create(shopId, dto);
            return Created($"/api/shop/{shopId}/product/{ProductId}", null);
        }

        [HttpPut("{productId}")]
        public ActionResult Update([FromRoute] int productId, [FromRoute] int shopId, [FromForm] CreateUpdateProductDto dto)
        {
            _productService.Update(productId, shopId,  dto);
            return Ok();
        }

        [HttpGet("{format}")]
        public ActionResult<List<ProductDto>> GetAll([FromRoute] int shopId, [FromQuery] PaginationQuery query)
        {
            var result = _productService.GetAll(shopId, query);
            return Ok(result);

        }

        [HttpGet("{productId}/{format}")]
        public ActionResult<ProductDto> Get([FromRoute] int shopId, [FromRoute] int productId)
        {
            var product = _productService.GetById(shopId, productId);
            return Ok(product);

        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{productId}")]
        public ActionResult<ProductDto> Delete([FromRoute] int shopId, [FromRoute] int productId)
        {
            _productService.Remove(shopId, productId);
            return NoContent();

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public ActionResult<ProductDto> DeleteAll([FromRoute] int shopId)
        {
            _productService.RemoveAll(shopId);
            return NoContent();

        }

    }
}
