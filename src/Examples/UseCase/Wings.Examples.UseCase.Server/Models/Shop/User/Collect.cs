using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Collect
    {
        public int Id { get; set; }
        public int WxUserId { get; set; }
        public int GoodId { get; set; }
        public int AddTime { get; set; }
        public bool IsAttention { get; set; }
        public int TypeId { get; set; }
    }
}
