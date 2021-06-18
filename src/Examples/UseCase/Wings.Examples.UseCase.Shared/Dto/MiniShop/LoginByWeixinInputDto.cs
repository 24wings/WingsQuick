using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Shared.Dto.MiniShop
{
    public class WexinLoginUserInfo
    {
        public string NickName { get; set; }
        public int Gender { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class WexinLoginUserInfoAll
    {
        public string encryptedData { get; set; }
        public string errMsg { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
        public WexinLoginUserInfo userInfo { get; set; }
    }
    public class LoginByWeixinInputDto
    {
        public string Code { get; set; }
        public WexinLoginUserInfoAll UserInfo { get; set; }
    }
}
