using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;

namespace Wings.Examples.UseCase.Server.Seed
{
    public static class SeedData
    {
        public static readonly List<Menu> allMenus =
         new List<Menu>
         {
             new Menu{Id=100,ParentId=null,Code="rbac",Name="角色权限",Icon="cloud",TreePath=",100,"},

             new Menu{Id=101,ParentId=100,Code="role",Name="角色管理",Icon="cloud",Url="/rbac/role",TreePath=",100,101,"},
             new Menu{Id=102,ParentId=100,Code="user",Name="用户管理",Icon="cloud",Url="/rbac/user", TreePath=",100,102,"},
             new Menu{Id=200,ParentId=null,Code="system",Name="系统设置",Icon="cloud",TreePath=",200,"},
             new Menu{Id=201,ParentId=200,Code="person-center",Name="UI设置",Icon="cloud",TreePath=",200,201,"},
             new Menu{Id=202,ParentId=200,Code="menu",Name="菜单管理",Icon="cloud",Url="/rbac/menu", TreePath=",200,202,"},


         };



        public static readonly List<Permission> allPermissions = new List<Permission>
        {
            new Permission{Id=0, Label="角色权限"}
        };


        /// <summary>
        /// 初始化开发者
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task InitializeDefaultDeveloperResource(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            // 删除数据库
            await context.Database.EnsureDeletedAsync();
            // 确认数据库已经创建
            await context.Database.EnsureCreatedAsync();

            // 加入菜单
            await context.Menus.AddRangeAsync(allMenus);
            var adminRole = new RbacRole { Id = 1, Code = "admin", NormalizedName = "admin", Name = "admin", Menus = allMenus };
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddAsync(adminRole);
                await context.SaveChangesAsync();
            }

            // 创建开发者公司
            //await context.companys.AddAsync(new Company { id = 1, name = "开发者公司", status = CompanyStatus.Approve, code = "developer", description = "负责开发,运维不同公司的业务系统", menuIds = string.Join(",", allMenus.Select(m => m.id)) });
            //await context.rbacRoles.AddAsync(new RbacRole { id = 1, name = "开发者", companyId = 1, menuIds = string.Join(",", allMenus.Select(m => m.id)) });
            //await context.rbacMenus.AddRangeAsync(allMenus);

            // 创建丁丁公司
            //await context.companys.AddAsync(new Company { id = 2, name = "钉钉公司", status = CompanyStatus.Approve, code = "dingding", description = "钉钉群扫描", menuIds = string.Join(",", dingdingMenus.Select(m => m.id)) });
            //await context.rbacRoles.AddAsync(new RbacRole { id = 200, name = "钉钉管理员", companyId = 2, menuIds = string.Join(",", dingdingMenus.Select(m => m.id)) });

            if (!await context.Users.AnyAsync())
            {
                var userStore = serviceProvider.GetRequiredService<UserManager<RbacUser>>();
                // 初始化开发者
                var result = await userStore.CreateAsync(new RbacUser { Email = "13419597065", UserName = "13419597065", nickname = "刺月无影", roleId = 1, companyId = 1 }, "Shadow2016..");
                var admin = await userStore.FindByNameAsync("13419597065");
                await userStore.AddToRoleAsync(admin, "admin");
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 初始化开发者公司
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task InitializeDeveloperCompany(IServiceProvider serviceProvider)
        {

        }
    }
}
