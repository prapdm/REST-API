using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Authorization;
using ShopApi.Models;
using ShopApi.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/shop/{format?}")]
    [FormatFilter]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private IShopService _shopService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ShopController(IShopService shopService, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _shopService = shopService;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<ShopDto>> GetAll([FromQuery] PaginationQuery query)
        {
            var shopDtos = _shopService.GetAll(query);
            return Ok(shopDtos);
        }

        [HttpGet("own")]
       
        public ActionResult<IEnumerable<ShopDto>> GetOwn([FromQuery] PaginationQuery query)
        {
            var user_Id = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var shopDtos = _shopService.GetOwn(query, user_Id);
            return Ok(shopDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ShopDto>> Get([FromRoute] int id)
        {
            var shopDto = _shopService.GetById(id);
            return Ok(shopDto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager", Policy = "CountryIsPoland")]
         
        public ActionResult CreateShop([FromBody] CreateShopDto dto)
        {
            //var autorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, null, "CountryIsPoland").Result;
            var id = _shopService.Create(dto);
            return Created($"/api/shop/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateShop([FromBody] UpdateShopDto dto, [FromRoute] int id)
        {
            _shopService.Update(dto, id);
            return Ok();

        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _shopService.Delete(id);
            return NoContent();

        }

    }
}
