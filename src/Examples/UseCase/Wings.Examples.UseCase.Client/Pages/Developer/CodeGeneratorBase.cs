using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Shared.Dto;

namespace Wings.Examples.UseCase.Client.Pages
{
    public class CodeGeneratorBase:ComponentBase
    {
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PagePath { get; set; }

        public bool HasPagePath { get; set; }

        public string MainModalFullName { get; set; }
        /// <summary>
        /// 页面类型 
        /// stable-table
        /// stable-tree
        /// master-order-table
        /// master-order-tree
        /// 
        /// </summary>
        public string PageType { get; set; } = "stable-table";

        public bool HasSearchBar { get; set; }

        public string SearchBarTypeFullName { get; set; }


        public string CreateFormModelFullName { get; set; }
        public string UpdateFormModelFullName { get; set; }

        protected EditContext editContext { get; set; }

        public List<Type> TypeOptions { get; set; } = new List<Type>();

        public CodeGeneratorBase CodeConfig { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            editContext = new EditContext(this);
            TypeOptions = Assembly.GetAssembly(typeof(ProvinceJson)).GetTypes().Where(type => type.IsAbstract == false&&type.IsInterface==false).ToList();

        }

        public void GenerateCode()
        {
            CodeConfig = (CodeGeneratorBase)editContext.Model;
        }





    }
}
