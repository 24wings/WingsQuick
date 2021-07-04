using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Services;
using Wings.Examples.UseCase.Shared.Dto.MiniShop;

namespace Wings.Examples.UseCase.Server.Controllers.miniShop
{
    [ApiController]
    [Route("/mini-shop/[controller]/[action]")]
    public class GoodsController:ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public GoodsController(AppDbContext _appDbContext,IMapper _mapper) { appDbContext = _appDbContext; mapper = _mapper; }
        [HttpGet]
        public async Task<object> Detail([FromQuery]int id) {
            var userModel = TokenService.Decode(HttpContext.Request.Headers["X-Nideshop-Token"].FirstOrDefault());
            bool userHasCollect = false;
            if (userModel != null)
            {
                userHasCollect = appDbContext.Collects.Where(collect => collect.WxUserId == userModel.ID && collect.GoodId == id).Any();
            }
           var  currentGood=   appDbContext.Goods.Where(good => good.Id == id).FirstOrDefault();
            
            if (currentGood == null)
            { 
                return null;
            }
           var gallery= appDbContext.GoodGallerys.Where(g => g.GoodId == id).Take(4).ToList();
            var attribute = appDbContext.GoodAttrs.Where(goodAttr => goodAttr.GoodId == id).ProjectTo<GoodAttrDto>(mapper.ConfigurationProvider).ToList();
            var issue = appDbContext.GoodIssues.ToList();
            var brand = appDbContext.Brands.Where(brand => brand.Id == currentGood.BrandId).FirstOrDefault();
            var commenCount = appDbContext.Comments.Where(comment => comment.GoodId == id && comment.TypeId == CommentType.Message).Count(); 
            
            var hotComment = appDbContext.Comments.Where(comment => comment.GoodId == id && comment.TypeId == CommentType.Message).FirstOrDefault();
            var issus = currentGood.Issues;
            var comment = new CommentInfoDto( new CommentDto("", new List<CommentPicture>()), commenCount);
            
            if (hotComment != null)
            {
                comment = new CommentInfoDto(new CommentDto(Base64Decode(hotComment.Content), hotComment.CommentPictures), commenCount);
            }
            return new  {
                Info=mapper.Map<Good,GoodInfoDto>(currentGood),
                Issue= issue,
                Attribute =attribute ,
                userHasCollect,
                brand,
                gallery=gallery,
                comment,
                specificationList=currentGood.GoodSpecifications,
                productList=currentGood.Products
            };
        }

        protected string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }

        [HttpGet]
        public int Count()
        {
            return appDbContext.Goods.Where(good => good.IsDelete == false && good.IsOnSale == true).Count();
        }

    }

    public class CommentDto
    {
        public string Content { get; }
        public List<CommentPicture> Pic_list { get; }

        public CommentDto(string content, List<CommentPicture> pic_list)
        {
            Content = content;
            Pic_list = pic_list;
        }

  

      
    }

    internal class CommentInfoDto
    {
        public CommentDto Data { get; }
        public int Count { get; }

        public CommentInfoDto(CommentDto data, int count)
        {
            Data = data;
            Count = count;
        }

     

    }
}
