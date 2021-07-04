using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Framework.Shared.Dtos.Admin;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Examples.UseCase.Client.Pages
{
    public class DynamicPageBase : ComponentBase
    {

        [Parameter]
        public string PageName { get; set; }
        public Type MainViewType { get; set; }

        public Type TabViewTypeList { get; set; }

        public PageData PageData { get; set; }
        
   
        protected override void OnInitialized()
        {

            var listPageType = Assembly.GetAssembly(typeof(Wings.Examples.UseCase.Shared.SharedAutoMapperProfile))
                  .DefinedTypes
                  .Where(type =>
                  type.IsClass
                  //&& type.IsSubclassOf(typeof(IListPage<,,,>))
                  && type.Name == PageName
                  ).FirstOrDefault();
            if (listPageType != null)
            {
                //MainViewType = listPageType.GetGenericArguments().FirstOrDefault();
                //TabViewTypeList = listPageType.GetGenericArguments().LastOrDefault();
                //Console.WriteLine(MainViewType);
                //Console.WriteLine(TabViewTypeList);

            }
            


        }


    }
}
