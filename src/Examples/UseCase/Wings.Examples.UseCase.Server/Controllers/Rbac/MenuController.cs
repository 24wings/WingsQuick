using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;
using Microsoft.AspNet.OData;
using AutoMapper.QueryableExtensions;
using Wings.Examples.UseCase.Server;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace Wings.Examples.UseCase.Server.Controllers
{
    [ApiController]
    [Route("/api/menu/[action]")]
    public class MenuController:TreeEntityControllerBase<Menu,MenuListDvo,MenuCreateDvo,MenuCreateDvo>
    {

        public AppDbContext appDbContext { get; set; }

    

        public MenuController(IMapper _mapper,UnitOfWork _unitOfWork,UserManager<RbacUser> _userManager):base(_unitOfWork,_mapper)
        {
           

        }







       
     

    
      

    }
}