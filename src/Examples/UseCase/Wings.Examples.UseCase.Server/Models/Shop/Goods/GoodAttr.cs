using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class GoodAttr
    {
        [Key]
        public int Id { get; set; }

        public int GoodId { get; set; }

        public virtual Good Good { get; set; }

        public int AttrId { get; set; }
        public virtual Attr Attr { get; set; }

        public string Value { get; set; }
    }
}
