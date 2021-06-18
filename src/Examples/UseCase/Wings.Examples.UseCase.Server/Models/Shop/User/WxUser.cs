using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public enum Gender
    {
        Man=1,
        Felman
    }
    public class WxUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public int Birthday { get; set; }
        public int RegisterTime { get; set; }

        public int LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public int WxUserLevelId { get; set; }

        public virtual WxUserLevel WxUserLevel { get; set; }
        public string Nickname { get; set; }
        public string Mobile { get; set; }

        public string RegisterIp { get; set; }
        public string Avatar { get; set; }

        public string WeixinOpenid { get; set; }

    }
}
