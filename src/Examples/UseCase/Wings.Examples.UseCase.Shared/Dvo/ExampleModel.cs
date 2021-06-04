using System;
using System.ComponentModel.DataAnnotations;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Shared.Dvo
{



    /// <summary>
    /// 年级
    /// </summary>
    public enum Grade
    {
        [Display(Name = "一年级")]
        Level1 = 1,
        [Display(Name = "二年级")]
        Level2,
        [Display(Name = "三年级")]
        Level3

    }


    public class ExampleModel
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名必填")]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Display(Name = "年龄")]
        [Required(ErrorMessage = "年龄必填")]
        [Range(0, 200, ErrorMessage = "年龄必须大于0,小于200")]
        public int age { get; set; }


        [Display(Name = "生日")]
        [Required(ErrorMessage = "生日必填")]

        public DateTime BirthDate { get; set; } = DateTime.Now;
        [Display(Name = "学习时间")]
        [Required]
        public DateRange StudyTime { get; set; }

        [Display(Name = "婚否")]
        public bool IsMarry { get; set; }

        [Display(Name = "年级")]

        [Required(ErrorMessage = "年级必填")]

        public Grade GradeLevel { get; set; }




    }
    /// <summary>
    /// 动态表单
    /// </summary>
    public class ExampleDynamicForm
    {
        [Display(Name = "姓名")]
        [Required]
        [StringLength(10, ErrorMessage = "名字太长")]
        public string Name { get; set; }
    }
    public class ComAttribute : Attribute
    {
        public Type type { get; set; }
        public ComAttribute(Type type)
        {
            this.type = type;
        }
    }


}