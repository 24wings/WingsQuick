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

             #region 电商菜单
             new Menu{Id=300,ParentId=0,Name="电商平台",Icon="cloud",TreePath=",300,"},
             new Menu{Id=310,ParentId=300,Name="商品标签",Icon="cloud",TreePath=",300,310,"},
             new Menu{Id=311,ParentId=310,Name="商品分类",Icon="cloud",TreePath=",300,310,311,",Url="/shop/tags/category"},
             new Menu{Id=312,ParentId=310,Name="商品特性",Icon="cloud",TreePath=",300,310,312,",Url="/shop/tags/attribute"},
             new Menu{Id=313,ParentId=310,Name="特性分类",Icon="cloud",TreePath=",300,310,313,",Url="/shop/tags/attribute-category"},
             new Menu{Id=320,ParentId=300,Name="商品管理",Icon="cloud",TreePath=",300,320,"},
             new Menu{Id=321,ParentId=320,Name="产品管理",Icon="cloud",TreePath=",300,320,321,",Url="/shop/goods/goods"},
             new Menu{Id=322,ParentId=320,Name="订单管理",Icon="cloud",TreePath=",300,320,322,"},




            #endregion



             #region 开发者菜单
             new Menu{Id=900,ParentId=null,Name="开发者",Icon="cloud",TreePath=",900,"},
             new Menu{Id=901,ParentId=900,Name="权限管理",Icon="cloud",TreePath=",900,901,",Url="/developer/permission"},
             new Menu{Id=902,ParentId=900,Name="代码生成",Icon="cloud",TreePath=",900,902,",Url="/Developer/InterfaceCodePage"}

             #endregion

             





         };



        public static readonly List<Permission> allPermissions = new List<Permission>
        {
            new Permission{Id=100, Label="页面设置",Value="PageSetting",TreePath=",100,"},
            new Permission{Id=101,Label="页面结构设置",Value="PageStructSetting",ParentId=100,TreePath=",100,101,"}
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
            var adminRole = new RbacRole { Id = 1, Code = "admin", NormalizedName = "admin", Name = "admin", Menus = allMenus,Permissions=new List<Permission> { } };
            var userRoles = new RbacRole { Id = 2, Code = "user", NormalizedName = "user", Name = "user", Menus = allMenus.Where(m => m.Id < 900).ToList() };
            await context.Permissions.AddRangeAsync(allPermissions);
            userRoles.Permissions = allPermissions;
            adminRole.Permissions = allPermissions;
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddAsync(adminRole);
                await context.Roles.AddAsync(userRoles);
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

                var result2 = await userStore.CreateAsync(new RbacUser { Email = "user", UserName = "user", nickname = "用户", roleId = 1, companyId = 1 }, "Shadow2016..");
                var user = await userStore.FindByNameAsync("user");
                await userStore.AddToRoleAsync(user, "user");
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
