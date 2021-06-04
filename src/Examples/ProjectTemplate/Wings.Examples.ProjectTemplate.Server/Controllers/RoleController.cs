using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wings.Api.Models;
using Wings.Shared.Dto;
using Wings.Shared.Dvo;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Wings.Framework.Shared.Dtos;

namespace Wings.Api.Controllers
{
    [ApiController]
    [Route("/api/[Controller]/[action]")]
    public class RoleController
    {
        IMapper mapper;
        public AppDbContext appDbContext { get; set; }

        public RoleController(AppDbContext _appDbContext, IMapper _mapper)
        {
            appDbContext = _appDbContext;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<BasicQueryResult<RoleListDvo>> Load([FromQuery] BasicQuery query)
        {
            var result = appDbContext.Roles.AsQueryable().Take(query.PageSize).Skip(query.PageIndex * query.PageSize).ToList();
            foreach (var item in result)
            {
                var menu = item.Menus.Where(menu => menu.Id == 1).FirstOrDefault();
                item.Menus = new List<Menu> { };
                if (menu != null) item.Menus.Add(menu);
            }
            var count = appDbContext.Roles.Count();
            var data = mapper.Map<List<Role>, List<RoleListDvo>>(result);
            return new BasicQueryResult<RoleListDvo>() { Data = data, Total = count };


        }
        [HttpGet]
        public async Task<object> Insert()
        {
            List<int> menuIds = new List<int>() { 1, 2 };
            var menus = appDbContext.Menus.Where(menu => menuIds.Contains(menu.Id)).ToList();
            var newRole = new Role() { Name = "管理员", Menus = menus };
            await appDbContext.Roles.AddAsync(newRole);
            await appDbContext.SaveChangesAsync();
            return true;

        }

        [HttpDelete]
        public async Task<object> Delete([FromQuery] int id)
        {
            var role = appDbContext.Roles.Where(role => role.Id == id).FirstOrDefault();
            appDbContext.Roles.Remove(role);
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}