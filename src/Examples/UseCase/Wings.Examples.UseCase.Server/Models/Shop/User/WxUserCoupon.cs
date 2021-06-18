using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class WxUserCoupon
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int CouponNumber { get; set; }
        public int UserId { get; set; }
        public int UsedTime { get; set; }
        public int OrderId { get; set; }
    }
}
