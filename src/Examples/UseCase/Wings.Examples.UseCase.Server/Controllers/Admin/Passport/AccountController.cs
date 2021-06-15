using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Examples.UseCase.Shared.Dvo;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Server.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private static UserModel LoggedOutUser = new UserModel { IsAuthenticated = false };

        private readonly UserManager<RbacUser> _userManager;
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public AccountController(IMapper _mapper, UserManager<RbacUser> userManager  , AppDbContext _appDbContext)
        {
            _userManager = userManager;
             appDbContext= _appDbContext ;
            mapper = _mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<UserInfoDto> GetMyUserInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var roleNames = await _userManager.GetRolesAsync(user);


                var roles = await appDbContext.Roles.Include(role => role.Menus)
                    .Include(role=>role.Permissions)
                    .Where(role => roleNames.Contains(role.NormalizedName)).ToListAsync();
                var allMenus = new List<Menu>();
                var allPermissions = new List<Permission>();

                roles.ForEach(role => { allMenus.AddRange(role.Menus);allPermissions.AddRange(role.Permissions); });
                var ids = allMenus.Select(menu => menu.Id).Distinct();
                var menus= await appDbContext.Menus.Where(menu => ids.Contains(menu.Id)).ProjectTo<MenuData>(mapper.ConfigurationProvider).ToListAsync();

                var permissionIds = allMenus.Select(menu => menu.Id).Distinct();
                var permissions = await appDbContext.Permissions.Where(permission => permissionIds.Contains(permission.Id)).ProjectTo<PermissionListDvo>(mapper.ConfigurationProvider).ToListAsync();



                return new UserInfoDto { MenuDataList = menus ,PermissionList=permissions,User=new UserDto { Nickname=user.nickname,Id=user.Id,Phone=user.PhoneNumber} };
            }
            else
            {
                return null;
            }
            


        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] RegisterModel model)
        //{
        //    var newUser = new RbacUser { UserName = model.Email, Email = model.Email };

        //    var result = await _userManager.CreateAsync(newUser, model.Password);
        //    var user = await _userManager.FindByNameAsync(model.Email);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name, model.Email),
        //    };
        //    await _userManager.AddClaimsAsync(user, claims);

        //    if (!result.Succeeded)
        //    {
        //        var errors = result.Errors.Select(x => x.Description);

        //        return BadRequest(new RegisterResult { Successful = false, Errors = errors });

        //    }

        //    return Ok(new RegisterResult { Successful = true });
        //}
    }
}
