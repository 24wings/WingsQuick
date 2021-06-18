using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models { 

    /// <summary>
    /// 商品属性
    /// </summary>
    public class Attr:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public  virtual int AttrCategoryId { get; set; }
        public  virtual AttrCategory AttrCategory { get; set; }

        public string Name { get; set; }
        public bool InputType { get; set; }

        public string Values { get; set; }

        public int SortOrder { get; set; }

    }
}
