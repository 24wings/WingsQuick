using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wings.Api.Models;
using System.Linq;
using Wings.Shared.Dto;
using Wings.Shared.Dvo;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Wings.Api.Controllers
{
    [ApiController]
    [Route("/api/menu/[action]")]
    public class MenuController
    {

        IMapper mapper;
        public AppDbContext appDbContext { get; set; }

        public MenuController(AppDbContext _appDbContext, IMapper _mapper)
        {
            appDbContext = _appDbContext;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<BasicQueryResult<MenuListDvo>> Load()
        {
            var menuList = await appDbContext.Menus.AsNoTracking().AsQueryable().Include(m => m.Children).FirstOrDefaultAsync();
            var data = new List<MenuListDvo>() { mapper.Map<Menu, MenuListDvo>(menuList) };

            return new BasicQueryResult<MenuListDvo> { Data = data, Total = 1 };
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
            subMenu.Parent = menu;
            menu.Children.Add(subMenu);
            appDbContext.SaveChanges();
            return true;
        }

    }
}