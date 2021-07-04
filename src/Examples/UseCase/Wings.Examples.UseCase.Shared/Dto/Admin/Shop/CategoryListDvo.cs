using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Attributes.FieldAttributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Shared.Dtos.Admin;

namespace Wings.Examples.UseCase.Shared.Dto
{

    /// <summary>
    /// CategoryListView
    /// </summary>
    [SearchBar(typeof(CategorySearchBar))]
    [DataGrid]
    [Display(Name="分类页面")]
    [DataSource("/api/AttrCategory")]
    public class CategoryListViewDto 
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
    //[Tab]
    public class CategoryDetailViewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class CategorySearchBar
    {
        public string Name { get; set; }
    }
    [SearchBar(typeof(CategorySearchBar))]
    [DataSource("/api/category")]
    public class CategoryListDvo : BasicTree<CategoryListDvo>
    {
        [FormField(Edit = false)]
        public int Id { get; set; }
        [FormField(Edit =false)]
        public int? ParentId { get; set; }
        public string Title { get; set; }
        [Display(Name ="关键字")]
        public string Keywords { get; set; }

        [Ignore]
        public List<CategoryListDvo> Children { get; set; }

        public string FrontDesc { get; set; }
        [Display(Name ="排序")]
        public int SortOrder { get; set; }
        [Display(Name ="展示下标")]
        public int ShowIndex { get; set; }
        [Display(Name ="是否显示")]
        public bool IsShow { get; set; }
        [FormFieldUploadImage]
        public string BannerUrl { get; set; }
        public string IconUrl { get; set; }

        public string ImgUrl { get; set; }

        public string WapBannerUrl { get; set; }

        public string Level { get; set; }

        public int Type { get; set; }

        public string FrontName { get; set; }
        [Ignore]
        public virtual List<AttrListDvo> Attrs { get; set; }

        

    }
    public class AttrSearchBar
    {
        [Where(WhereCondition.Contains)]
        public string Name { get; set; }
    }

    [SearchBar(typeof(AttrSearchBar))]
    [DataSource("/api/attr")]
    public class AttrListDvo
    {
        [Key]
        [FormField(Edit = false)]
        public int Id { get; set; }
       
        [FormFieldSelect()]
        [IgnoreColumn]
        public AttrCategoryOption AttrCategoryOption { get; set; }
        [IgnoreField]
        [IgnoreColumn]
        public int? AttrCategoryId { get { return AttrCategoryOption?.Id; } }

        [IgnoreField]
        public string AttrCategoryOptionName { get { return AttrCategoryOption?.Label; } }
        public string Name { get; set; }
        public bool InputType { get; set; }
        public int SortOrder { get; set; }


        public AttrListDvo() { }

        public static AttrListDvo CreateAttrListDvo(AttrListDvo item,AttrCategoryOption option)
        {
            item.AttrCategoryOption = option;
            return item;

        }



    }
    [DataSource("/api/AttrCategory/",LoadUrl= "/api/AttrCategory/Options")]
    public class AttrCategoryOption : ILabel
    {
        public int Id { get; set; }

        public string Label { get; set; }


    }
    public class AttrCategorySearchBar
    {
        [Where(WhereCondition.Contains)]
        public string Name { get; set; }
    }

    [SearchBar(typeof(AttrCategorySearchBar))]
    [DataSource("/api/AttrCategory")]
    public class AttrCategoryListDvo
    {
        public int Id { get; set; }
        [Display(Name = "属性分类")]
        public string Name { get; set; }
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }


    }
}
