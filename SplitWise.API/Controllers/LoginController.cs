using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SplitWise.Business;
using SplitWise.Business.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System.Security.Claims;

namespace SplitWise.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (ModelState.IsValid)
            {


                var userExist = await _loginService.userFindByEmail(register.Email);
                if (userExist != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, new Model.RequestModels.Response { Status = "Error", Message = "User Already Exist" });
                }
                SplitUser user = new()
                {
                    Email = register.Email,
                    UserName = register.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    
                };
                var result = await _loginService.Createuser(user, register.Password);
                if (result.Succeeded)
                {
                    Model.RequestModels.RegisterRequest  responseRegister = await _loginService.GetResponseRegister(user, register);
                    return Ok(responseRegister);
                }
                else
                {
                    return BadRequest("Registration failed due to validation errors");
                }
            }
            else
            {
                return BadRequest("Registration failed due to validation errors");
            }

        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Users users)
        {
            var user = await _loginService.userFindByEmail(users.Email);
            if (user != null && await _loginService.userCheckPassword(user, users.Password))
            {
                TokenResponse response = _loginService.GetTokenResponse(user);
                return Ok(response);
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _loginService.GetGetUsers();
         
            if (users == null )
            {
                StatusCode(404, new { error = "Users not found" });
            }

            return Ok(new { users });
        }
    }
}
