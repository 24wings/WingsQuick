using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wings.Examples.UseCase.Shared.Dto
{
    public class ProvinceJson
    {
        public string name { get; set; }
        public List<CityJson> city { get; set; }

    }

    public class CityJson
    {
        public string name { get; set; }

        public List<string> area { get; set; }

    }
    public class RbacUserModel
    {
        public string Email { get; set; }

        public string nickname { get; set; }

        public long companyId { get; set; }

        public long roleId { get; set; }

        public string Sign { get; set; }
        public string Summary { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }
        public string City { get; set; }

        public string AvatarUrl { get; set; }

    }
    public class MyMenu
    {
        public long id { get; set; }
        public string text { get; set; }

        public string link { get; set; }
        public List<MyMenu> childrens { get; set; }
        public long parentId { get; set; }

        public string icon { get; set; }
    }
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} 必须最少 {2} 且 最大 {1} 字符长度.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        [Display(Name = "验证码")]
        public string Code { get; set; }
    }
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
    public class LoginModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名必填")]
        public string Email { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Password { get; set; }
        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }

    public class BasicTree
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual List<BasicTree> Children { get; set; }
        public virtual string Icon { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual object OriginData { get; set; }
    }

    public interface IBasicTree
    {
        int Id { get; set; }
        string Title { get; set; }
        List<BasicTree> Children { get; set; }
        string Icon { get; set; }
        int? ParentId { get; set; }
        object OriginData { get; set; }
    }

}