using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Services.Repositorys;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Category: TreeEntity
    {
        [Key]
        public override int Id { get; set; }

        public string Name { get; set; }

        public string Keywords { get; set; }
        public string FrontDesc { get; set; }

        public override int? ParentId { get; set; } = 0;
        
        public int SortOrder { get; set; }

        public int ShowIndex { get; set; }


        public bool IsShow { get; set; }

        public string BannerUrl { get; set; }

        public string IconUrl { get; set; }

        public string ImgUrl { get; set; }

        public string WapBannerUrl { get; set; }

        public string Level { get; set; }

        public int Type { get; set; }

        public string FrontName { get; set; }

 

        public override string TreePath { get; set; }

        public virtual List<Good> Goods { get; set; }
    }
}
