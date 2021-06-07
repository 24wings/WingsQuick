using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;

namespace Wings.Examples.UseCase.Server.Services.Repositorys
{
    public class BaseTreeRepository<T> : BaseRepository<T> where T : TreeEntity
    {
        public BaseTreeRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public IQueryable<T> GetChildren(T item)
        {


            return appDbContext.Set<T>().Where(record => record.TreePath.Contains(item.TreePath));

        }

        public IQueryable<T> GetChildrenById(int Id)
        {
            var menu =  appDbContext.Set<T>().FirstOrDefault(item => item.Id == Id);

            return GetChildren(menu);
        }
        /// <summary>
        /// 获得上级元素
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAncestors(T item)
        {
            var itemTreePath = item.TreePath;
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(item.TreePath))
            {
                ids = item.TreePath.Split(",").Where(str=>!string.IsNullOrEmpty(str)).Select(idChar => int.Parse(idChar)).ToList();
            }

            return appDbContext.Set<T>().Where(record => ids.Contains(record.Id));
        }

        public IQueryable<T> GetAncestorsById(int id)
        {
            var menu =appDbContext.Set<T>().FirstOrDefault(data=>data.Id==id);
           return GetAncestors(menu);
        }

        public async Task<T> CreateTopAsync(T item)
        {
            item.TreePath = ",,";
            var result = await appDbContext.Set<T>().AddAsync(item);
            await appDbContext.SaveChangesAsync();
            var record = appDbContext.Set<T>().FirstOrDefault(data => data.Id == result.Entity.Id);
            record.TreePath = "," + record.Id + ",";
            appDbContext.Set<T>().Update(record);
            await appDbContext.SaveChangesAsync();
            return record;

        }

        public async Task<T> CreateAsync(T item)
        {
            if (item.ParentId == 0 || item.ParentId == null)
            {
                return await CreateTopAsync(item);
            }
            else
            {
                return await AddChildByParentId(item, item.ParentId.Value);

            }
        }

        public async Task<T> AddChild(T newChild,T parent)
        {
               var  item= await appDbContext.Set<T>().AddAsync(newChild);
            await appDbContext.SaveChangesAsync();
            newChild.TreePath = parent.TreePath + "," +item.Entity.Id;

            appDbContext.Update(newChild);
            await appDbContext.SaveChangesAsync();
            return newChild;

        }

        public async Task<T> AddChildByParentId(T newChild, int ParentId)
        {
            
            var parent =  appDbContext.Set<T>().FirstOrDefault(menu => menu.Id == ParentId);
            return await AddChild(newChild, parent);

        }



    }
}
