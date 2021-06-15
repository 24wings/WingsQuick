using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using Wings.Examples.UseCase.Shared.Dto;

namespace Wings.Examples.UseCase.Server.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : TreeEntityControllerBase<Category, CategoryListDvo, CategoryListDvo, CategoryListDvo>
    {
        public CategoryController(UnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
        }

       
    }
}
