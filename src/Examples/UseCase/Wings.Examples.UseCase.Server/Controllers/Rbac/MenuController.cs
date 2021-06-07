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
    public class MenuController:TreeEntityControllerBase<Menu>
    {

        IMapper mapper;
        public AppDbContext appDbContext { get; set; }

        private readonly UserManager<RbacUser> userManager;

        public MenuController(AppDbContext _appDbContext, IMapper _mapper,UnitOfWork _unitOfWork,UserManager<RbacUser> _userManager):base(_unitOfWork)
        {
            appDbContext = _appDbContext;
            mapper = _mapper;
            userManager = _userManager;
        }




        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public new IQueryable<MenuListDvo> Load()
        {
            return base.Load().ProjectTo<MenuListDvo>(mapper.ConfigurationProvider);
        }


        [HttpGet]
        public async Task<MenuData> LoadMenuSegmentsByMenuId()
        {
            var menu = await appDbContext.Menus.AsQueryable().FirstAsync(m => m.Id == 8);

            return mapper.Map<Menu, MenuData>(menu);

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
           var roles= await appDbContext.Roles.Where(role => roleNames.Contains(role.Name)).Include(role=>role.Menus).ToListAsync();
            var allMenus = new List<Menu>();
            roles.ForEach(role => allMenus.AddRange(role.Menus));
            var ids = allMenus.Select(menu => menu.Id).Distinct();
            return await appDbContext.Menus.Where(menu => ids.Contains(menu.Id)).ProjectTo<MenuData>(mapper.ConfigurationProvider).ToListAsync();

            //return await appDbContext.Menus.ProjectTo<MenuData>(mapper.ConfigurationProvider).ToListAsync();

        }
        [HttpGet]
        public Menu CreateTop()
        {
            var top = new Menu { Name = "顶级菜单" };
            appDbContext.Menus.Add(top);
            appDbContext.SaveChanges();
            return top;
        }
        public bool AddSub()
        {
            var menu = appDbContext.Menus.FirstOrDefault();
            var subMenu = new Menu { Name = "二级菜单" };
            appDbContext.SaveChanges();
            return true;
        }

        [HttpPost]
        public async Task<object> Insert([FromBody] MenuListDvo menu)
        {
          return await base.CreateAsync(mapper.Map<MenuListDvo, Menu>(menu));

        }
        [HttpPost]
        public async Task<object> Update([FromBody] MenuCreateDvo menu)
        {

            var currentMenu = await appDbContext.Menus.FirstAsync(m => menu.Id == m.Id);
            currentMenu.Name = menu.Title;
            currentMenu.Code = menu.Code;
            currentMenu.Url = menu.Path;
            appDbContext.Menus.Update(currentMenu);
            await appDbContext.SaveChangesAsync();
            return true;

        }
        [HttpDelete]
        public async Task<object> Delete([FromQuery] int id)
        {
            var menu = await appDbContext.Menus.FirstOrDefaultAsync(menu => menu.Id == id);
            if (menu != null)
            {
                appDbContext.Menus.Remove(menu);
                await appDbContext.SaveChangesAsync();
            }
            return true;


        }

    }
}