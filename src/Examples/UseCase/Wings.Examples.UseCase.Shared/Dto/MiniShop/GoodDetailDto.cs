using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Shared.Dto.MiniShop
{
    public class GoodAttrDto
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }
    

   

    public class GoodInfoDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
        public string GoodsSn { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int GoodsNumber { get; set; }

        public string Keywords { get; set; }
        public string GoodsBrief { get; set; }
        public string GoodsDesc { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime AddTime { get; set; }
        public int SortOrder { get; set; }
        public bool IsDelete { get; set; }
        public int? AttributeCategoryId { get; set; }
        //public virtual AttrCategoryDto AttributeCategory { get; set; }
        public decimal CounterPrice { get; set; }

        public decimal ExtraPrice { get; set; }
        public bool IsNew { get; set; }
        public string GoodsUnit { get; set; }
        public string PrimaryPicUrl { get; set; }
        public string ListPicUrl { get; set; }
        public decimal retail_price { get; set; }
        public int SellVolume { get; set; }
        public int PrimaryProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string PromotionDesc { get; set; }
        public string PromotionTag { get; set; }

        public decimal app_exclusive_price { get; set; }

        public bool IsAppExclusive { get; set; }
        public int IsLimited { get; set; }
        public bool IsHot { get; set; }


    }


}
