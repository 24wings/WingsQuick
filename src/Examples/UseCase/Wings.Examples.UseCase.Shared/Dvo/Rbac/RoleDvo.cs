using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Examples.UseCase.Shared.Attributes;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Shared.Dvo
{
    public class RoleListSearchBar
    {
        [Display(Name = "角色名")]
        [Where(WhereCondition.Contains)]
        public string Name { get; set; }
        [Display(Name = "角色代码")]
        [Where(WhereCondition.Contains)]
        public string Code { get; set; }
        [Display(Name = "创建时间")]
        public DateRange CreateDateBetween
        {
            get { return new DateRange(CreateAtStart, CreateAtEnd); }
            set { CreateAtStart = value.Start; CreateAtEnd = value.End; }
        }
        [Ignore]
        [Where(WhereCondition.GreatThen, nameof(RoleListDvo.CreateAt))]
        public DateTime? CreateAtStart { get; set; }
        [Ignore]
        [Where(WhereCondition.LessThen, nameof(RoleListDvo.CreateAt))]
        public DateTime? CreateAtEnd { get; set; }
    }
    [SearchBar(typeof(RoleListSearchBar))]
    [DataSource("/api/Role")]
    [TablePage("角色管理")]
    [Display(Name = "角色管理")]
    public class RoleListDvo
    {
        [Key]
        [FormField(Edit = false)]
        public int Id { get; set; }


        [Display(Name = "角色名")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "角色代码")]
        [Required]

        public string Code { get; set; }

        [PropTreeView]
        [TreeSelectField("/api/Menu/Load")]
        [Display(Name = "角色菜单")]
        public List<MenuListDvo> MenuList { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

    }

    public class MenuTreeSelectDvo
    {

    }
}