using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int GoodId { get; set; }

        public virtual Good Good { get; set; }
        public string GoodSpecificationIds { get; set; }

        public string GoodSn { get; set; }

        public int GoodsNumber { get; set; }

        public decimal RetailPrice { get; set; }


    }
}
