using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services;

namespace Wings.Examples.UseCase.Server.Controllers.miniShop
{
    public class CommentPostDto
    {
        public string content { get; set; }
        public int valueId { get; set; }
        public int TypeId { get; set; }
    }
    [ApiController]
    [Route("mini-shop/[controller]/[action]")]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public CommentController(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        [HttpGet]
        public object list(CommentType typeId, int valueId, int showType, int size = 5, int page = 0)
        {
            // 不显示图片
            if (showType != 1)
            {
                var comments = appDbContext.Comments.Where(comment => comment.TypeId == typeId && comment.GoodId == valueId).Take(size).Skip(page * size).ToList();
                var commentsCount = appDbContext.Comments.Where(comment => comment.TypeId == typeId && comment.GoodId == valueId).Count();
                return new { data = comments, count = commentsCount };

            }
            else
            {
                var comments = appDbContext.Comments.Include(comment => comment.CommentPictures).Where(comment => comment.TypeId == typeId && comment.GoodId == valueId).Take(size).Skip(page * size).ToList();
                var commentsCount = appDbContext.Comments.Where(comment => comment.TypeId == typeId && comment.GoodId == valueId).Count();
                return new {errno=0, data = comments, count = commentsCount };
            }

        }

        [HttpPost]
        public async Task<object> Post(CommentPostDto dto)
        {
            var userModel = TokenService.DecodeByHttpContext(HttpContext);
            if (userModel != null)
            {
                
                await appDbContext.Comments.AddAsync(
                    new Comment { 
                    Content = dto.content,
                        GoodId = dto.valueId,
                        TypeId = dto.TypeId==0?CommentType.Message:CommentType.Reply,
                        WxUserId=userModel.ID});
                await appDbContext.SaveChangesAsync();
                return new { errno = 0, message = "评论成功" };
            }
            else
            {
                return new { errno = 1, message = "尚未登录" };
            }

        }
    }
}
