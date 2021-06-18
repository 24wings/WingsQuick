using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public enum CommentType
    {
        Message = 0,
        Reply
    }
    public class Comment
    {
        public int Id { get; set; }
        public CommentType TypeId { get; set; }
        public int GoodId { get; set; }
        public string Content { get; set; }
        public int AddTime { get; set; }
        public bool Status { get; set; }
        public int WxUserId { get; set; }
        public string NewContent { get; set; }
        public virtual List<CommentPicture> CommentPictures { get; set; }
    }
}
