using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class CommentPicture
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public string PicUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
