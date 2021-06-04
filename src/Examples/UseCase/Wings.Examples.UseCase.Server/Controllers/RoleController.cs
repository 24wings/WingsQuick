using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wings.Api.Models;
using Wings.Shared.Dto;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using AutoMapper.QueryableExtensions;
using Wings.Examples.UseCase.Server;

namespace Wings.Examples.UseCase.Server.Controllers
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
        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public IQueryable<RoleListDvo> Load([FromQuery] ODataUri query)
        {
            var result = appDbContext.Roles.Include(r => r.Menus).ProjectTo<RoleListDvo>(mapper.ConfigurationProvider);
            // foreach (var item in result)
            // {
            //     var menu = item.Menus.Where(menu => menu.Id == 1).FirstOrDefault();
            //     item.Menus = new List<Menu> { };
            //     if (menu != null) item.Menus.Add(menu);
            // }
            // var count = appDbContext.Roles.Count();
            // var data = mapper.Map<IQueryable<Role>, IQueryable<RoleListDvo>>(result);
            // return new BasicQueryResult<RoleListDvo>() { Data = data, Total = count };
            return result;


        }
        [HttpGet]
        public async Task<object> Insert()
        {
            var menus = appDbContext.Menus.Where(menu => menu.Id == 1).ToList();
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