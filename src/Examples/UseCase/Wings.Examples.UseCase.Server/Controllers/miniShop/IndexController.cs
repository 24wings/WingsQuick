using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Shared.Dto.MiniShop;

namespace Wings.Examples.UseCase.Server.Controllers.miniShop
{
    [ApiController]
    [Route("/mini-shop/[controller]/[action]")]
    public class IndexController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public IndexController(AppDbContext _appDbContext, IMapper _mapper) { appDbContext = _appDbContext; mapper = _mapper; }

        /// <summary>
        /// 缓存尚未实现
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IndexPageDto Index()
        {
            var banner = appDbContext.Ads.Where(ad => ad.AdPositionId == 1).ProjectTo<AdDto>(mapper.ConfigurationProvider).ToList();
            var topicList = appDbContext.Topics.ProjectTo<TopicDto>(mapper.ConfigurationProvider).Take(3).ToList();
            var channels = appDbContext.Channels.ProjectTo<ChannelDto>(mapper.ConfigurationProvider).OrderBy(c => c.SortOrder).Take(3).ToList();
            var brandList = appDbContext.Brands.ProjectTo<BrandDto>(mapper.ConfigurationProvider).Take(3).ToList();
            var categoryList = appDbContext.Categories.ProjectTo<CategoryDto>(mapper.ConfigurationProvider).Where(cate => cate.ParentId == 0 && cate.Name != "推荐").ToList();

            var newGoods = appDbContext.Goods.Where(good => good.IsNew).ProjectTo<GoodDto>(mapper.ConfigurationProvider).Take(4).ToList();
            var hotGoods = appDbContext.Goods.Where(good => good.IsHot).ProjectTo<GoodDto>(mapper.ConfigurationProvider).Take(3).ToList();
            var newCategoryList = new List<CategoryDto> { };
            foreach (var parent in categoryList)
            {
                var childCategoryIds = appDbContext.Categories.Where(categoryItem => categoryItem.ParentId == parent.Id).Select(item => item.Id);

                var goods = appDbContext.Goods.Where(good => childCategoryIds.Contains(good.CategoryId)).ProjectTo<GoodDto>(mapper.ConfigurationProvider).ToList();
                newCategoryList.Add(new CategoryDto { GoodsList = goods, Id = parent.Id, Name = parent.Name });
            }
            //        .where({ parent_id: 0, name:['<>', '推荐']}).select();
            //    const newCategoryList = [];
            //    for (const categoryItem of categoryList) {
            //        const childCategoryIds = await this.model('category').where({ parent_id: categoryItem.id}).getField('id', 100);
            //        const categoryGoods = await this.model('goods').field(['id', 'name', 'list_pic_url', 'retail_price']).where({ category_id:['IN', childCategoryIds]}).limit(7).select();
            //        newCategoryList.push({
            //        id: categoryItem.id,
            //name: categoryItem.name,
            //goodsList: categoryGoods
            //        });
            //    }

            return new IndexPageDto
            {
                Message = "ok",
                Banner = banner,
                TopicList = topicList,
                Channel = channels,
                CategoryList = newCategoryList,
                BrandList = brandList,
                NewGoodsList = newGoods,
                HotGoodsList = hotGoods
            };
        }
    }
}
