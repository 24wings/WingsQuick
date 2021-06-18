using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class GoodGallery
    {
        public int Id { get; set; }

        public int GoodId { get; set; }

        public virtual Good Good { get; set; }

        public string ImageUrl { get; set; }

        public string ImgDesc { get; set; }

        public int SortOrder { get; set; }
    }
}
