using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Examples.UseCase.Shared.Attributes;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Shared.Dto;

namespace Wings.Examples.UseCase.Shared.Dvo
{
    [CrudModel(
        Create = typeof(MenuCreateDvo),
        Update = typeof(MenuCreateDvo)
    )]
    [DataSource("/api/Menu")]
    public class MenuListDvo:BasicTree<MenuListDvo>
    {
        [Display(Name = "Id")]
        public  int Id { get; set; }
        [Display(Name = "菜单名")]
        public string Title { get; set; }
        [Display(Name = "菜单代码")]
        public string Code { get; set; }
        [Display(Name = "上级Id")]
        public  int? ParentId { get; set; }
        [Display(Name = "链接")]
        public string Path { get; set; }

        [Display(Name = "图标")]
        public string Icon { get; set; }
        public List<MenuListDvo> Children { get; set; } = new List<MenuListDvo>();
    }
    [DataSource("/api/Menu")]
    [Display(Name = "新建菜单")]
    public class MenuCreateDvo
    {
        public int Id { get; set; }
        [Display(Name = "菜单名")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "菜单编码")]
        [Required]
        public string Code { get; set; }
        [Display(Name = "上级菜单")]
        [Editable(false)]
        public int ParentId { get; set; }
        [IconPickerField]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        [Display(Name = "链接")]
        public string Path { get; set; }
    }



}