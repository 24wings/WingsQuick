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
using Wings.Examples.UseCase.Server.Services.UnitOfWork;

namespace Wings.Examples.UseCase.Server.Controllers
{
    [ApiController]
    [Route("/api/[Controller]/[action]")]
    public class RoleController:TableEntityControllerBase<RbacRole,RoleListDvo,RoleListDvo,RoleListDvo> 
    {
        public AppDbContext appDbContext { get; set; }

        public RoleController(AppDbContext _appDbContext, IMapper _mapper,UnitOfWork unitOfWork):base(unitOfWork,_mapper)
        {
            appDbContext = _appDbContext;
        }
       
      

        [HttpPost]
        public override async Task<bool> Update([FromBody] RoleListDvo roleListDvo)
        {
          var dbRole= await appDbContext.Roles.FirstOrDefaultAsync(role => role.Id==roleListDvo.Id);
            var role = mapper.Map<RoleListDvo, RbacRole>(roleListDvo);
            var menuIds = role.Menus.Select(m => m.Id).ToList();
            var permissionIds = role.Permissions.Select(m => m.Id).ToList();
            role.Name = dbRole.Name;
            role.NormalizedName = dbRole.NormalizedName;
            role.Code = dbRole.Code;
            // 删除以前的关联关系

            dbRole.Menus.Clear();
            dbRole.Permissions.Clear();
            // 从数据库关联新的关联关系
            dbRole.Menus = appDbContext.Menus.Where(menu => menuIds.Contains(menu.Id)).ToList();
            dbRole.Permissions = appDbContext.Permissions.Where(permission => permissionIds.Contains(permission.Id)).ToList();
            appDbContext.Roles.Update(dbRole);
            await appDbContext.SaveChangesAsync();

         

            return true;
        }

        
    }
}