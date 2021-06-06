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
            var itemTreePath = item.TreePath;
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(item.TreePath))
            {
                 ids = item.TreePath.Split(",").Select(idChar => int.Parse(idChar)).ToList();
                Console.WriteLine("ids:"+ids.Count);


            }

            Console.WriteLine(string.Join(",",ids)+ids.Count+":"+item.TreePath);
                return appDbContext.Set<T>().Where(record => ids.Contains(record.Id));
            
        }



    }
}
