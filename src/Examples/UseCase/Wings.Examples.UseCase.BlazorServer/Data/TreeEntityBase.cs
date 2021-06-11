using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.BlazorServer.Data
{
 
    public abstract class TreeEntityBase
    {
        public virtual int Id { get; set; }
        public virtual string TreePath { get; set; }
        public virtual int? ParentId { get; set; }

        public TreeEntityBase()
        {

        }

    }
}
