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
    public class PermissionController : TreeEntityControllerBase<Permission,PermissionListDvo,PermissionListDvo,PermissionListDvo>
    {
        public PermissionController(UnitOfWork _unitOfWork,IMapper _mapper) : base(_unitOfWork,_mapper)
        {
        }

        
    }
}
