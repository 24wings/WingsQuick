using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;
using Microsoft.AspNet.OData;
using AutoMapper.QueryableExtensions;
using Wings.Examples.UseCase.Server;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace Wings.Examples.UseCase.Server.Controllers
{
    [ApiController]
    [Route("/api/menu/[action]")]
    public class MenuController:TreeEntityControllerBase<Menu,MenuListDvo,MenuCreateDvo,MenuCreateDvo>
    {

        public AppDbContext appDbContext { get; set; }

        private readonly UserManager<RbacUser> userManager;

        public MenuController(AppDbContext _appDbContext, IMapper _mapper,UnitOfWork _unitOfWork,UserManager<RbacUser> _userManager):base(_unitOfWork,_mapper)
        {
            appDbContext = _appDbContext;
            userManager = _userManager;
        }







        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<List<MenuData>> My()
        {
          var user= await userManager.FindByNameAsync(User.Identity.Name);
           var roleNames= await userManager.GetRolesAsync(user);

            
           var roles= await appDbContext.Roles.Include(role => role.Menus).Where(role => roleNames.Contains(role.NormalizedName)).ToListAsync();
            var allMenus = new List<Menu>();
            roles.ForEach(role => allMenus.AddRange(role.Menus));
            var ids = allMenus.Select(menu => menu.Id).Distinct();
            Console.WriteLine( ids.Count());
            return await appDbContext.Menus.Where(menu => ids.Contains(menu.Id)).ProjectTo<MenuData>(mapper.ConfigurationProvider).ToListAsync();


        }
     

    
      

    }
}