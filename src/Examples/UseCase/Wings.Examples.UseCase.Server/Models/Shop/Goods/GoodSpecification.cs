using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{


  

    public class GoodSpecification
    {
        public int Id { get; set; }
        public int GoodId { get; set; }
        public virtual Good Good { get; set; }
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
        public string Value { get; set; }
        public string PicUrl { get; set; }
    }
}
