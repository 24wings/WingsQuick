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
namespace Wings.Examples.UseCase.Server.Controllers
{
    [ApiController]
    [Route("/api/menu/[action]")]
    public class MenuController:ControllerBase
    {

        IMapper mapper;
        public AppDbContext appDbContext { get; set; }
        private readonly UnitOfWork unitOfWork;
     

        public MenuController(AppDbContext _appDbContext, IMapper _mapper,UnitOfWork _unitOfWork)
        {
            appDbContext = _appDbContext;
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        public async Task<IList<Menu>> LoadTest()
        {
            var paged = await unitOfWork.GetRepository<Menu>().Select();
            return paged;
        }
        [HttpGet]
        public async Task<IList<Menu>> LoadChildrenTest()
        {
          var menu=  await unitOfWork.GetRepository<Menu>().Select(menu => menu.Id == 2);
            Console.WriteLine(JsonSerializer.Serialize(menu));

           var menuChilren= await unitOfWork.GetTreeRepository<Menu>().GetChildren(menu[0]).ToListAsync();
            return menuChilren;

        }
        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public IQueryable<MenuListDvo> Load()
        {
            return appDbContext.Menus.ProjectTo<MenuListDvo>(mapper.ConfigurationProvider);
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
           var claims= User.Claims.ToList();

            Console.WriteLine(claims.Count+":"+User.Identity.Name+":"+User.Identity.IsAuthenticated);
            foreach(var calim in claims)
            {
                Console.WriteLine(calim.Value);
            }
            return await appDbContext.Menus.ProjectTo<MenuData>(mapper.ConfigurationProvider).ToListAsync();

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
        public async Task<object> Insert([FromBody] MenuCreateDvo menu)
        {

            var parent = await appDbContext.Menus.FirstAsync(m => menu.ParentId == m.Id);
            var newMenu = new Menu { ParentId = menu.ParentId, Name = menu.Title, Code = menu.Code, Url = menu.Path };
            await appDbContext.Menus.AddAsync(newMenu);
            await appDbContext.SaveChangesAsync();
            return true;

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