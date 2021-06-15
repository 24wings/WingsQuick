using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Shared.Dto;

namespace Wings.Examples.UseCase.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<RbacUser> _signInManager;
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<RbacUser> _userManager;

        public LoginController(IConfiguration configuration,
            SignInManager<RbacUser> signInManager,
            AppDbContext _applicationDbContext,
             UserManager<RbacUser> userManager
            )
        {
            _configuration = configuration;
            _signInManager = signInManager;
            applicationDbContext = _applicationDbContext;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "用户名或密码错误" });


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
            var user = await _userManager.FindByNameAsync(login.Email);
           var claims = await _userManager.GetClaimsAsync(user);
            
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) } );
        }
        [Authorize]
        [HttpGet("[action]")]
        public async  Task<bool> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            return false;
            
        }
    }
}
