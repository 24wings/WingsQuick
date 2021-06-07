using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services.Repositorys;

namespace Wings.Examples.UseCase.Server.Services.UnitOfWork
{
    
    public class UnitOfWork
    {
        public AppDbContext appDbContext { get; set; }
        public UnitOfWork(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public BaseRepository<T> GetRepository<T> () where T:class
        {
            return new BaseRepository<T>(appDbContext);
        }
        public BaseTreeRepository<T> GetTreeRepository<T>() where T : TreeEntity
        {
            return new BaseTreeRepository<T>(appDbContext);
        }

    }
}
