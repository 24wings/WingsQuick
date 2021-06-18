using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public enum CouponSendType
    {
        User,
        Product,
        Order,
        Offline
    }
    public class Coupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TypeMoney { get; set; }
        public CouponSendType SendType { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int SendStartDate { get; set; }
        public int SendEndDate { get; set; }
        public int UseStartDate { get; set; }
        public int UseEndDate { get; set; }
        public decimal MinGoodsAmount { get; set; }
    }
}
