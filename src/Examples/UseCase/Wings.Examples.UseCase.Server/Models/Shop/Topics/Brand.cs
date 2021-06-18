using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Mame { get; set; }
        public string ListPicUrl { get; set; }
        public string SimpleDesc { get; set; }
        public string PicUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsShow { get; set; }
        public decimal FloorPrice { get; set; }
        public string AppListPicUrl { get; set; }
        public bool IsNew { get; set; }
        public string NewPicUrl { get; set; }
        public int NewSortOrder { get; set; }

    }
}
