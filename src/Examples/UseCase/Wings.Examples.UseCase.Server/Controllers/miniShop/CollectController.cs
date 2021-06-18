using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services;

namespace Wings.Examples.UseCase.Server.Controllers.miniShop
{
  public  class AddOrderDeleteDto
    {
        public int TypeId { get; set; }

        public int ValueId { get; set; }
    }
    [ApiController]
    [Route("mini-shop/[controller]/[action]")]
    public class CollectController : ControllerBase
    {


        private readonly AppDbContext appDbContext;
        public CollectController(AppDbContext _appContext) { appDbContext = _appContext; }
        [HttpPost]
        public async Task<object> Addordelete([FromBody] AddOrderDeleteDto dto)
        {
            var userModel = TokenService.DecodeByHttpContext(HttpContext);
            if (userModel != null)
            {
                var isCollect = appDbContext.Collects.Where(collect => collect.WxUserId == userModel.ID && collect.TypeId == dto.TypeId && collect.Id == dto.ValueId).FirstOrDefault();

                // 尚未收藏，开始收藏
                if (isCollect == null)
                {
                    appDbContext.Collects.Add(new Collect { WxUserId = userModel.ID, TypeId = dto.TypeId, GoodId = dto.ValueId });
                }
                // 已收藏,取消收藏
                else
                {
                    appDbContext.Collects.Remove(isCollect);
                }
            }
            else
            {

                return NotFound();
            }


            await appDbContext.SaveChangesAsync();
            return new { errno = 0, data = true };

        }
    }
}
