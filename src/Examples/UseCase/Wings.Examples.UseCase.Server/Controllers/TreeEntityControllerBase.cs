using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Services.Repositorys;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;

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
         return await  unitOfWork.GetTreeRepository<T>().CreateAsync(item);
        }
        public virtual IQueryable<T> Load()
        {
            return  unitOfWork.GetRepository<T>().AsQueryable();
            
        }
        public async Task<T> CreateTop(T item)
        {
            return await unitOfWork.GetTreeRepository<T>().CreateTopAsync(item);

        }
        public  IQueryable<T> LoadChildren([FromQuery] int id)
        {
            return  unitOfWork.GetTreeRepository<T>().GetChildrenById(id);
        }

        public  IQueryable<T> LoadAncestors(int id)
        {
            return  unitOfWork.GetTreeRepository<T>().GetAncestorsById(id);
        }

        public async Task<T> AddMenuChild(T newItem,int ParentId)
        {
            return await unitOfWork.GetTreeRepository<T>().AddChildByParentId(newItem, ParentId);
        }



    }
}
