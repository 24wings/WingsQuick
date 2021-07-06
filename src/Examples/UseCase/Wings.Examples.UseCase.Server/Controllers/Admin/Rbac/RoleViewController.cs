using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using Wings.Examples.UseCase.Shared.Dvo;

namespace Wings.Examples.UseCase.Server.Controllers.Admin.Rbac
{
    [ApiController]
    [Route("/api/[Controller]/[action]")]
    public class RoleViewController : TableEntityControllerBase<RbacRole, RoleListView, RoleListView, RoleListView>
    {
        public AppDbContext appDbContext { get; set; }

        public RoleViewController(AppDbContext _appDbContext, IMapper _mapper, UnitOfWork unitOfWork) : base(unitOfWork, _mapper)
        {
            appDbContext = _appDbContext;
        }
        [HttpPost]
        public override async Task<RoleListView> Insert([FromBody]RoleListView input)
        {
          var role=  mapper.Map<RoleListView, RbacRole>(input);
            
            role.Menus = appDbContext.Menus.Where(m => role.Menus.Select(m => m.Id).Contains(m.Id)).ToList();
            role.Permissions = appDbContext.Permissions.Where(m => role.Permissions.Select(p => p.Id).Contains(m.Id)).ToList();
            appDbContext.Roles.Add(role);
            await appDbContext.SaveChangesAsync();
            return  mapper.Map<RbacRole,RoleListView>(role);
        }


        [HttpPost]
        public override async Task<bool> Update([FromBody] RoleListView roleListDvo)
        {
            var dbRole = await appDbContext.Roles.FirstOrDefaultAsync(role => role.Id == roleListDvo.Id);
            var role = mapper.Map<RoleListView, RbacRole>(roleListDvo);
            var menuIds = role.Menus.Select(m => m.Id).ToList();
            var permissionIds = role.Permissions.Select(m => m.Id).ToList();
            role.Name = dbRole.Name;
            role.NormalizedName = dbRole.NormalizedName;
            role.Code = dbRole.Code;
            // 删除以前的关联关系

            dbRole.Menus.Clear();
            dbRole.Permissions.Clear();
            await appDbContext.SaveChangesAsync();
            // 从数据库关联新的关联关系
            dbRole.Menus = appDbContext.Menus.Where(menu => menuIds.Contains(menu.Id)).ToList();
            dbRole.Permissions = appDbContext.Permissions.Where(permission => permissionIds.Contains(permission.Id)).ToList();
            appDbContext.Roles.Update(dbRole);
            await appDbContext.SaveChangesAsync();



            return true;
        }

        [HttpGet]
        public   RoleDetailView Detail(int id)
        {
           return appDbContext.Roles.Where(role => role.Id == id).ProjectTo<RoleDetailView>(mapper.ConfigurationProvider).FirstOrDefault();
        }


    }
}
