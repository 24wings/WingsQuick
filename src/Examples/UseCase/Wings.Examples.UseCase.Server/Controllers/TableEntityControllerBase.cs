using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services.Repositorys;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;

namespace Wings.Examples.UseCase.Server.Controllers
{
    public class TableEntityControllerBase<TEntity, TListDto, TCreateDto, TUpdateDto> :ControllerBase
        where TEntity : class,BaseEntity
    {
        protected readonly UnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public TableEntityControllerBase(UnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public virtual IQueryable<TListDto> Load()
        {
            return unitOfWork.GetRepository<TEntity>().AsQueryable().ProjectTo<TListDto>(mapper.ConfigurationProvider);

        }
        [HttpDelete]
        public virtual async Task<bool> Delete([FromQuery] int id)
        {
            var record = await unitOfWork.GetRepository<TEntity>().GetEntity((item) => item.Id == id);
            if (record != null)
            {
                await unitOfWork.GetRepository<TEntity>().Delete((item) => item.Id == id);
                await unitOfWork.appDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpPost]
        public virtual async Task<bool> Update([FromBody] TUpdateDto item)
        {
            var updateEntity = mapper.Map<TUpdateDto, TEntity>(item);


            unitOfWork.GetRepository<TEntity>().Update(updateEntity);
            await unitOfWork.appDbContext.SaveChangesAsync();
            return true;

        }


        [HttpPost]
        public virtual async Task<TCreateDto> Insert([FromBody] TCreateDto item)
        {
            var newRecord = mapper.Map<TCreateDto, TEntity>(item);
            var entity = await unitOfWork.GetRepository<TEntity>().Insert(newRecord);
            await unitOfWork.appDbContext.SaveChangesAsync();
            return mapper.Map<TEntity, TCreateDto>(entity.Entity);
        }
    }
}
