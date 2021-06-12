using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Client.Pages
{
    public class CodeBase : ComponentBase
    {
        [Parameter]
        public CodeGeneratorBase CodeConfig { get; set; }

        protected RenderFragment dynamicCodeComponent => builder =>
        {
            if (CodeConfig != null)
            {
                Type PageComponent = null;
                var assembly = System.Reflection.Assembly.Load("Wings.Examples.UseCase.Shared");
                var mainModel = assembly.GetType(CodeConfig.MainModalFullName);
                var createForm = assembly.GetType(CodeConfig.CreateFormModelFullName);
                var updateFormModel = assembly.GetType(CodeConfig.UpdateFormModelFullName);
                Console.WriteLine(mainModel);
                Console.WriteLine(createForm);
                Console.WriteLine(updateFormModel);



                switch (CodeConfig.PageType)
                {
                    case "stable-table":
                        PageComponent = typeof(TablePageCodeTemplate<object, object, object>).GetGenericTypeDefinition().MakeGenericType(
                           mainModel,
                            createForm,
                            updateFormModel
                            );
                        Console.WriteLine(PageComponent);
                        break;
                    case "stable-tree":
                        PageComponent = typeof(TreeViewPageCodeTemplate<object, object, object>).GetGenericTypeDefinition().MakeGenericType(
                         mainModel,
                          createForm,
                          updateFormModel
                          );
                        break;
                }
                builder.OpenComponent(0, PageComponent);
                builder.AddAttribute(1,"CodeConfig", CodeConfig);
                builder.CloseComponent();

            }
        };
    }

}
