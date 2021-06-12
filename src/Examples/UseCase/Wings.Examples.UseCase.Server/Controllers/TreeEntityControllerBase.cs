using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Services.Repositorys;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Server.Controllers
{

    public abstract class TreeEntityControllerBase<T> : ControllerBase where T : TreeEntity
    {
        protected readonly UnitOfWork unitOfWork;
        public TreeEntityControllerBase(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public virtual async Task<T> CreateAsync(T item)
        {
            return await unitOfWork.GetTreeRepository<T>().CreateAsync(item);
        }

        public virtual IQueryable<T> Load()
        {
            return unitOfWork.GetRepository<T>().AsQueryable();

        }
        public async Task<T> CreateTop(T item)
        {
            return await unitOfWork.GetTreeRepository<T>().CreateTopAsync(item);

        }
        public IQueryable<T> LoadChildren([FromQuery] int id)
        {
            return unitOfWork.GetTreeRepository<T>().GetChildrenById(id);
        }

        public IQueryable<T> LoadAncestors(int id)
        {
            return unitOfWork.GetTreeRepository<T>().GetAncestorsById(id);
        }

        public async Task<T> AddMenuChild(T newItem, int ParentId)
        {
            return await unitOfWork.GetTreeRepository<T>().AddChildByParentId(newItem, ParentId);
        }



    }


    public abstract class TreeEntityControllerBase<TEntity, TListDto, TCreateDto, TUpdateDto> : TableEntityControllerBase<TEntity, TListDto, TCreateDto, TUpdateDto>
        where TEntity : TreeEntity
    {
     
        public TreeEntityControllerBase(UnitOfWork _unitOfWork, IMapper _mapper):base(_unitOfWork,_mapper)
        {
        }

        [HttpPost]
        public  override async Task<TCreateDto> Insert([FromBody] TCreateDto item)
        {
            var newRecord = mapper.Map<TCreateDto, TEntity>(item);
            var entity = await unitOfWork.GetTreeRepository<TEntity>().CreateAsync(newRecord);
            return mapper.Map<TEntity, TCreateDto>(entity);
        }

        [HttpPost]
        public override async Task<bool> Update([FromBody] TUpdateDto item)
        {
          return  await base.Update(item);

        }

        [AsyncQuery]
        [EnableQuery]
        [HttpGet]
        public override IQueryable<TListDto> Load()
        {
            return base.Load();

        }
        [HttpPost]
        public async Task<TCreateDto> CreateTop([FromBody] TCreateDto item)
        {
            var newRecord = mapper.Map<TCreateDto, TEntity>(item);

            var data = await unitOfWork.GetTreeRepository<TEntity>().CreateTopAsync(newRecord);
            return mapper.Map<TEntity, TCreateDto>(data);

        }

        [HttpGet]
        public IQueryable<TListDto> LoadChildren([FromQuery] int id)
        {
            var children = unitOfWork.GetTreeRepository<TEntity>().GetChildrenById(id);
            return mapper.Map<IQueryable<TEntity>, IQueryable<TListDto>>(children);
        }

        [HttpGet]
        public virtual IQueryable<TListDto> LoadAncestors([FromQuery] int id)
        {
            var ancestors = unitOfWork.GetTreeRepository<TEntity>().GetAncestorsById(id);
            return mapper.Map<IQueryable<TEntity>, IQueryable<TListDto>>(ancestors);

        }


        [HttpPost]
        public async Task<TListDto> AddChild([FromBody] TCreateDto item, int ParentId)
        {
            var newRecord = mapper.Map<TCreateDto, TEntity>(item);
            var result = await unitOfWork.GetTreeRepository<TEntity>().AddChildByParentId(newRecord, ParentId);
            return mapper.Map<TEntity, TListDto>(result);
        }
       



    }
}
