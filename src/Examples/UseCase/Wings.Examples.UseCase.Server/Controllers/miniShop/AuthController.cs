using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services;
using Wings.Examples.UseCase.Shared.Dto.MiniShop;

namespace Wings.Examples.UseCase.Server.Controllers.miniShop
{
    [ApiController]
    [Route("mini-shop/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public AuthController(AppDbContext _appDbContext) { appDbContext = _appDbContext; }
        [HttpPost]
        public async  Task<object> LoginByWeixin([FromBody] LoginByWeixinInputDto inputDto)
        {
            var code = inputDto.Code;
            //:todo  微信注册将用户code转为openId
            var openId = code;
            var isUserExsit = appDbContext.WxUsers.Where(user => user.WeixinOpenid == code).Any();

            if (!isUserExsit)
            {
                appDbContext.WxUsers.Add(new WxUser
                {
                    Nickname = inputDto.UserInfo.userInfo.NickName,
                    Password = "",
                    RegisterIp = HttpContext.Connection.RemoteIpAddress.ToString(),
                    WeixinOpenid = openId,
                    Avatar = inputDto.UserInfo.userInfo.AvatarUrl,
                    Gender = inputDto.UserInfo.userInfo.Gender == 1 ? Gender.Man : Gender.Felman,
                    WxUserLevelId = 1

                }) ;
                await appDbContext.SaveChangesAsync();
            }
            var user = appDbContext.WxUsers.Where(user => user.WeixinOpenid == openId).Select(user => new { Id = user.Id, Nickname = user.Nickname, Avatar = user.Avatar }).FirstOrDefault();

            var token = TokenService.GetJWT(new TokenModel { ID = user.Id, Name = user.Nickname });

            return new {data= new { token, userInfo = user }, errno=0 };



        }
    }
}
