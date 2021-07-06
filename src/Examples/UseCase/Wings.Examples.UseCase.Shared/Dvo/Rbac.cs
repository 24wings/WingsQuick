using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Examples.UseCase.Shared.Attributes;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Shared.Dtos.Admin;

namespace Wings.Examples.UseCase.Shared.Dvo
{
    public class MenuPage : PageDesign
    {
        public override PageData Design()
        {
        return    SetMainViewName(MainViewName.AntTreeView)
                  .SetPageTitle("菜单管理")
                 .SetPageLink("/rbac/menu2")
                 .SetMainView<MenuListDvo>()
                 .SetCreateViewType<MenuCreateDvo>()
                 .SetUpdateViewType<MenuCreateDvo>()
                 .SetDetailViewType<MenuListDvo>()
                 .Commit();
                 
                 
        }
    }

    [CrudModel(
        Create = typeof(MenuCreateDvo),
        Update = typeof(MenuCreateDvo)
    )]
    [DataSource("/api/Menu",PageSize =100)]
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
        public int? ParentId { get; set; }
        [IconPickerField]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        [Display(Name = "链接")]
        public string Path { get; set; }
    }



}