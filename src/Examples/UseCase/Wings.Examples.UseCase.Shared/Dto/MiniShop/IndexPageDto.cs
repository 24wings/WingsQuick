using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Shared.Dto.MiniShop
{
    public class AdDto
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string link { get; set; }
        public string IconUrl { get; set; }

    }
    public class TopicDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public string Avatar { get; set; }

        public string ItemPicUrl { get; set; }

        public string SubTitle { get; set; }


        public decimal PriceInfo { get; set; }


        public string ReadCount { get; set; }

        public string ScenePicUrl { get; set; }
        public int TemplateId { get; set; }

        public int TopicTagId { get; set; }

        public int SortOrder { get; set; }

        public bool IsShow { get; set; }
    }
    public class BrandDto
    {
        public int Id { get; set; }
        public string Mame { get; set; }
        public string ListPicUrl { get; set; }
        public string SimpleDesc { get; set; }
        public string PicUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsShow { get; set; }
        public decimal FloorPrice { get; set; }
        public string AppListPicUrl { get; set; }
        public bool IsNew { get; set; }
        public string NewPicUrl { get; set; }
        public int NewSortOrder { get; set; }


    }
    public class ChannelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public int SortOrder { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ListPicUrl { get; set; }
        public string RetailPrice { get; set; }
        public int ParentId { get; set; }
        //public List<CategoryDto> Children { get; set; }

        public List<GoodDto> GoodsList { get; set; }
    }
    public class GoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ListPicUrl { get; set; }

        public decimal RetailPrice { get; set; }
        public string GoodsBrief { get; set; }
    }
    public class IndexPageDto
    {

        public List<AdDto> Banner { get; set; }
        public string Message { get; set; }


        public List<TopicDto> TopicList { get; set; }

        public List<BrandDto> BrandList { get; set; }
        public List<ChannelDto> Channel { get; set; }
        public List<CategoryDto> CategoryList { get; set; }

        public List<GoodDto> NewGoodsList { get; set; }
        public List<GoodDto> HotGoodsList { get; set; }
    }
}
