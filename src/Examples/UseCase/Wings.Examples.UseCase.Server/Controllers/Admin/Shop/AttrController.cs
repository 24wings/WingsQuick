using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class AttrController : TableEntityControllerBase<Attr, AttrListDvo, AttrListDvo, AttrListDvo>
    {
        public AttrController(UnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
        }


        [HttpPost]
        public override async Task<AttrListDvo> Insert([FromBody]AttrListDvo dvo)
        {

            var item = mapper.Map<AttrListDvo, Attr>(dvo);
           var attrcategory=  unitOfWork.appDbContext.AttrCategories.FirstOrDefault(item => item.Id == dvo.AttrCategoryOption.Id);
            item.AttrCategory = attrcategory;
            unitOfWork.appDbContext.Attrs.Update(item);
            await unitOfWork.appDbContext.SaveChangesAsync();
            return dvo;

             
        }



    }
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttrCategoryController : TableEntityControllerBase<AttrCategory, AttrCategoryListDvo, AttrCategoryListDvo, AttrCategoryListDvo>
    {
        public AttrCategoryController(UnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
        }
        [HttpGet]
        public async Task<bool> ToggleEnable([FromQuery] int id, [FromQuery] bool enable)
        {
            var attrCategory = unitOfWork.appDbContext.AttrCategories.Where(cate => cate.Id == id).FirstOrDefault();
            if (attrCategory != null)
            {
                attrCategory.Enable = enable;
                unitOfWork.appDbContext.AttrCategories.Update(attrCategory);
                await unitOfWork.appDbContext.SaveChangesAsync();
                return true;

            }
            else
            {
                return false;
            }
        }


        [EnableQuery]
        [AsyncQuery]
        [HttpGet]
        public override IQueryable<AttrCategoryListDvo> Load()
        {
            return unitOfWork.appDbContext.AttrCategories.ProjectTo<AttrCategoryListDvo>(mapper.ConfigurationProvider);
        }

    }
}
