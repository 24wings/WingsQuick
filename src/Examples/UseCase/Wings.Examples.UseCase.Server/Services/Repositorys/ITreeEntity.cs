using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Services.Repositorys
{
    public class TreeEntity
    {
        public virtual int Id { get; set; }
        public virtual string TreePath { get; set; }
        public virtual int? ParentId { get; set; }

        public TreeEntity()
        {

        }
      
    }
}
