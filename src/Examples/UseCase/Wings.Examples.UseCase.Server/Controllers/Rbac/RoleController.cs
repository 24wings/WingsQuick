using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wings.Examples.UseCase.Shared.Dto;
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
using Wings.Examples.UseCase.Server.Models;
using Newtonsoft.Json;

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
            var result = appDbContext.Roles.ProjectTo<RoleListDvo>(mapper.ConfigurationProvider);
         
            return result;


        }
        [HttpGet]
        public async Task<List<MenuListDvo>> RoleMenus([FromQuery]int Id)
        {
          var role=await   appDbContext.Roles.FirstOrDefaultAsync(role => role.Id == Id);
           return mapper.Map<List<Menu>, List<MenuListDvo>>(role.Menus);

        }
        [HttpGet]
        public async Task<List<PermissionListDvo>> RolePermissions([FromQuery] int Id)
        {
           var role= await appDbContext.Roles.FirstOrDefaultAsync(role => role.Id == Id);
            return mapper.Map<List<Permission>, List<PermissionListDvo>>(role.Permissions);
        }

        [HttpPost]
        public async Task<object> Insert([FromBody] RoleListDvo roleListDvo)
        {
            var role = mapper.Map<RoleListDvo, RbacRole>(roleListDvo);
            await appDbContext.Roles.AddAsync(role);
            await appDbContext.SaveChangesAsync();
            return true;

        }
        [HttpPost]
        public async Task<bool> Update([FromBody] RoleListDvo roleListDvo)
        {
          var dbRole= await appDbContext.Roles.FirstOrDefaultAsync(role => role.Id==roleListDvo.Id);
            var role = mapper.Map<RoleListDvo, RbacRole>(roleListDvo);
            var menuIds = role.Menus.Select(m => m.Id).ToList();
            var permissionIds = role.Permissions.Select(m => m.Id).ToList();
            role.Name = dbRole.Name;
            role.NormalizedName = dbRole.NormalizedName;
            role.Code = dbRole.Code;
            dbRole.Menus.Clear();
            dbRole.Permissions.Clear();
            // 删除以前的关联关系
            dbRole.Menus = appDbContext.Menus.Where(menu => menuIds.Contains(menu.Id)).ToList();
            dbRole.Permissions = appDbContext.Permissions.Where(permission => permissionIds.Contains(permission.Id)).ToList();
            appDbContext.Roles.Update(dbRole);
            await appDbContext.SaveChangesAsync();

            // 添加新的关联菜单 权限
            //dbRole.Menus.AddRange(role.Menus);
            //dbRole.Permissions.AddRange(role.Permissions);
            //appDbContext.Roles.Update(dbRole);
            //await appDbContext.SaveChangesAsync();

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