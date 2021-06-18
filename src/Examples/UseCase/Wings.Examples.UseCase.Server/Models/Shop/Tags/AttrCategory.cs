using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class AttrCategory:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Enable { get; set; }

        public virtual List<Attr> Attrs { get; set; }

    }
}
