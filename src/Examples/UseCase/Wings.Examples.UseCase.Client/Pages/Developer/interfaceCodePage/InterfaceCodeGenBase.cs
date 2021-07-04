using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos.Admin;

namespace Wings.Examples.UseCase.Client.Pages
{
    public class InterfaceCodeGenBase : ComponentBase
    {
        [Parameter]
        public PageData PageData { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        public string GetImport()
        {
            return @$"
@page ""{PageData.PageLink}""
@namespace Wings.Examples.UseCase.Client.Pages
@inject HttpClient HttpClient
@inject ModalService _modalService
@inject IMapper mapper

<DataSourceManager TModel=""{PageData.MainViewType.Name}"" @ref=""DataSource""> </DataSourceManager>
<AntTableView TModel=""{PageData.MainViewType.Name}"" @ref=""table"">
<ToolbarTemplate> <Button OnClick = ""()=>createForm.ShowModal(new { PageData.CreateViewType.Name }())"" Type = ""primary"" > 新增</Button > </ToolbarTemplate>
    <ActionColumnTemplate>
            <Space>
            <SpaceItem>
            <Space>

            <SpaceItem> 
                <Button Size=""small"" OnClick=""()=>updateForm.ShowModal(mapper.Map<{PageData.MainViewType.Name},{PageData.CreateViewType.Name}>(context))"" > 编辑  </Button> 
            </SpaceItem>
            <SpaceItem> 
                <Button Size=""small"" Danger OnClick =""async ()=> {{ SelectedData = context; OpenDeleteConfirmModal();}}"" > 删除 </Button> 
            </SpaceItem>
            </Space>
            </SpaceItem>
            </Space>
    </ActionColumnTemplate>

            </AntTableView>


<AntDynamicForm mode=""modal"" TModel=""{PageData.UpdateViewType.Name}"" @ref=""updateForm"" OnSubmit=""async () =>{{await updateForm.UpdateAsync();await table.Load(); }}""></AntDynamicForm>

@code {{
    public AntTableView<{PageData.MainViewType.Name}> table{{get;set;}}
    public {PageData.MainViewType.Name} SelectedData {{ get; set; }}
        AntDynamicForm<{PageData.MainViewType.Name}> createForm;
    AntDynamicForm<{PageData.MainViewType.Name }> updateForm;
    public DataSourceManager<{PageData.MainViewType.Name}> DataSource {{ get; set; }}
    public void OpenDeleteConfirmModal()
    {{
        _modalService.Confirm(new ConfirmOptions()
        {{
            Title = ""确定删除该条记录 ? "",
            OnOk = async (e) =>
            {{
                await DataSource.Delete(SelectedData);
                await table.Load();
            }},
            OnCancel = (e) => null
        }});
    }}

}}

";
        }

        public async Task CopyCode(string id)
        {
            await jSRuntime.InvokeVoidAsync("clipboardCopy.copyText", new object[] { "client-razor-code" });
        }


        public string GetCreateView()
        {
            if (PageData.CreateViewTabs != null)
            {
                if (PageData.CreateViewTabs.Count > 0)
                {
                    return GetCreateTabsViewCode();
                }
            }
            return GetCreateFormViewCode();

        
        }

        public string GetCreateFormViewCode()
        {
            return @"<AntDynamicForm mode=""modal"" TModel=""{PageData.CreateViewType.Name}"" @ref=""createForm"" OnSubmit=""async () =>{{await createForm.InsertAsync();await table.Load(); }}""></AntDynamicForm>";
        }

        public string GetCreateTabsViewCode()
        {
            return @$"@if(editData != null)
{{

    <Modal Visible=""editData!=null"" Width=""900"" Title=""编辑角色"" OnOk=""HandleOk"" OnCancel=""HandleCancel"">
        <Tabs>
            <TabPane Key=""1"">
                <Tab>基本信息</Tab>
                <ChildContent>
                    <AntDynamicForm TModel=""RoleListDvo"" Value=""editData""></AntDynamicForm>
                </ChildContent>
            </TabPane>
            <TabPane Key=""2"">
                <Tab>角色菜单</Tab>
                <ChildContent>
                    <AntTreeView Checkable=""true"" @bind-CheckedNodes=""editData.Menus"" TModel=""MenuListDvo""></AntTreeView>
                </ChildContent>
            </TabPane>
            <TabPane Key=""3"">
                <Tab>角色权限</Tab>
                <ChildContent>
                    <AntTreeView Checkable=""true"" @bind-CheckedNodes=""editData.Permissions"" TModel=""PermissionListDvo""></AntTreeView>
                </ChildContent>
            </TabPane>
        </Tabs>

    </Modal>
}}";
        }
    }
}
