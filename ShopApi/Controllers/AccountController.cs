using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using ShopApi.Services;

namespace ShopApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]  
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok("The user has been added, but need to activate by administrator.");
        }

        [HttpPost("activate")]
        [Authorize(Roles = "Administrator")]
        public ActionResult ActivateUser([FromBody] ActivateUserDto dto)
        {
            _accountService.ActivateUser(dto);
            return Ok($"Status of user {dto.Email} has beed set to {dto.IsActive}");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

    }
}
