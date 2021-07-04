using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos.Admin;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Examples.UseCase.Client.Pages
{
        public class InterfaceModel
    {
        public string Title { get; set; }
        public Type Type { get; set; }


        public bool IsNamespace { get; set; } = true;

        public List<InterfaceModel> Children { get; set; }

    }

    public class InterfaceCodePageBase : ComponentBase
    {

        protected string searchKey;



        public List<InterfaceModel> InterfaceModels { get; set; }
        public InterfaceModel selectedData { get; set; }

  

        protected override void OnInitialized()
        {
        var pages=    Assembly.GetAssembly(typeof(Wings.Examples.UseCase.Shared.SharedAutoMapperProfile)).DefinedTypes.Where(type => type.IsSubclassOf(typeof(PageDesign)));
            InterfaceModels = pages.GroupBy(page => page.Namespace).Select(group =>
            new InterfaceModel
            {
                IsNamespace = true,
                Title = group.Key,
                Children = group.Select(t => GetInterfaceModelByType(t)).ToList()
            }).ToList();




        }
        public PageData GetPageData()
        {
            return selectedData!=null&&selectedData.IsNamespace==false?((PageDesign)System.Activator.CreateInstance(selectedData.Type)).Design():null;

        }
    

        public InterfaceModel GetInterfaceModelByType(Type type)
        {
            Console.WriteLine(type.FullName);
          
           
            return new InterfaceModel {Type=type, Title=type.Name,IsNamespace=false };

        }
        protected RenderFragment dynamicCodeComponent => builder =>
        {
            if (selectedData != null){
                Type PageComponent = null;
                var assembly = System.Reflection.Assembly.Load("Wings.Examples.UseCase.Shared");
                //var mainModel = selectedData.MainViewType;
                //var createForm = selectedData.CreateViewType;
                //var updateFormModel = selectedData.UpdateViewType;
                //Console.WriteLine(mainModel);
                //Console.WriteLine(createForm);
                //Console.WriteLine(updateFormModel);
                



                //switch (CodeConfig.PageType)
                //{
                    //case "stable-table":
                        //PageComponent = typeof(InterfaceCodeGen).GetGenericTypeDefinition().MakeGenericType(
                        //   mainModel,
                        //    createForm,
                        //    updateFormModel,
                        //    selectedData.DetailViewType
                        //    );
                Console.WriteLine(PageComponent);
                //break;
                //case "stable-tree":
                //    PageComponent = typeof(TreeViewPageCodeTemplate<object, object, object>).GetGenericTypeDefinition().MakeGenericType(
                //     mainModel,
                //      createForm,
                //      updateFormModel
                //      );
                //    break;
                //}
                builder.OpenComponent(0, PageComponent);
                //builder.AddAttribute(1, "CodeConfig", CodeConfig);
                builder.CloseComponent();

            }
        };

    }

}
