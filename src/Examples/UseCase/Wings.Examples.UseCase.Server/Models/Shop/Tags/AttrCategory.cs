using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class AttrCategory:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Enable { get; set; }

    }
}
