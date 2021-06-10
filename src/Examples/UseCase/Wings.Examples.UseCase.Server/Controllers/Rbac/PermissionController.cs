using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using Wings.Examples.UseCase.Shared.Dvo;

namespace Wings.Examples.UseCase.Server.Controllers.Rbac
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PermissionController : TreeEntityControllerBase<Permission>
    {
        IMapper mapper;
        public PermissionController(UnitOfWork _unitOfWork,IMapper _mapper) : base(_unitOfWork)
        {
            mapper = _mapper;
        }

        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public new  IQueryable<PermissionListDvo> Load()
        {
            return base.Load().Select(per=>mapper.Map<Permission,PermissionListDvo>(per));

        }
        [HttpPost]
        public async Task<bool> Insert([FromBody] PermissionListDvo permissionListDvo)
        {
            var permission = mapper.Map<PermissionListDvo, Permission>(permissionListDvo);

            await base.CreateAsync(permission);
            return true;
        }
        [HttpDelete]
        public async Task<object> Delete([FromQuery] int id)
        {
            var item = await unitOfWork.appDbContext.Permissions.FirstOrDefaultAsync(record => record.Id == id);
            if (item != null)
            {
                unitOfWork. appDbContext.Permissions.Remove(item);
                await unitOfWork.appDbContext.SaveChangesAsync();
            }
            return true;


        }
    }
}
