using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;

namespace Wings.Examples.UseCase.Server.Services.Repositorys
{
    public class TreeEntity:BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual string TreePath { get; set; }
        public virtual int? ParentId { get; set; }

        public TreeEntity()
        {

        }
      
    }
}
