using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wings.Api.Models;
using Wings.Shared.Dto;
using Wings.Shared.Dvo;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

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
            var count = appDbContext.Roles.Count();
            var data = mapper.Map<List<Role>, List<RoleListDvo>>(result);
            return new BasicQueryResult<RoleListDvo>() { Data = data, Total = count };


        }
        [HttpGet]
        public async Task<object> Insert()
        {
            var menus = appDbContext.Menus.ToList();
            var newRole = new Role() { Name = "管理员", Menus = menus };
            await appDbContext.Roles.AddAsync(newRole);
            await appDbContext.SaveChangesAsync();
            return true;

        }
    }
}