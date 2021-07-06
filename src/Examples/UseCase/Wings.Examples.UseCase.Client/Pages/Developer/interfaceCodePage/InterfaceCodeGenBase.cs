using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Shared.Attributes;
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
{GetMainViewCode()}
{GetCreateView()}
{GetUpdateView()}


@if (SelectedData != null)
{{
    <Tabs>
        <TabPane Key=""1"">
            <Tab>基本信息</Tab>
            <ChildContent>
                <AntDetailView TModel=""{PageData.DetailViewType.Name}"" Value=""SelectedData""></AntDetailView>
            </ChildContent>
        </TabPane>
    
      {GetAllDetailTabCodes()}

    </Tabs>

}}


@code {{
    public {PageData.MainViewName}<{PageData.MainViewType.Name}> table{{get;set;}}
    public {PageData.MainViewType.Name} SelectedData {{ get; set; }}
    public {PageData.CreateViewType.Name} CreateData {{ get; set; }}
    public {PageData.UpdateViewType.Name} EditData {{ get; set; }}
    AntDynamicForm<{PageData.CreateViewType.Name}> createForm;
    AntDynamicForm<{PageData.UpdateViewType.Name }> updateForm;
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
{GetCreateDataSourceCode()}


 public async Task SelectData({PageData.MainViewType.Name} data)
    {{


        SelectedData = null;
        await Task.Run(async () =>
         {{
             await Task.Delay(20);
             SelectedData = data;
             StateHasChanged();
         }});



    }}



}}

";
        }
    

        public async Task CopyCode(string id)
        {
            await jSRuntime.InvokeVoidAsync("clipboardCopy.copyText", new object[] { "client-razor-code" });
        }

        public string GetCreateDataSourceCode()
        {
            if (IsCreateTabs())
            {
                return @$"
    public async Task HandleOk()
            {{
                if (CreateData.Id != 0)
                {{
                    await DataSource.Update(CreateData);
                }}
                else
                {{
                    await DataSource.Insert(CreateData);
                }}

            EditData = null;
            CreateData = null;
                await table.Load();
            }}";
            }
            else
            {
                return "";
            }
            
        }
        
        public string GetCreateButton()
        {
            if (IsCreateTabs())
            {
                return @$"<Button  OnClick = ""()=>{{CreateData= new {PageData.CreateViewType.Name}();}}"" Type = ""primary"" > 新增</Button >";
            }
            else
            {
               return @$"<Button OnClick = ""()=>createForm.ShowModal(new { PageData.CreateViewType.Name }())"" Type = ""primary"" > 新增</Button >";
            }
        }
        public string GetUpdateButton()
        {
            if (IsUpdateTabs())
            {
                return @$"<Button Size=""small"" OnClick = ""()=>EditData= mapper.Map<{PageData.MainViewType.Name},{PageData.UpdateViewType.Name}>(context)"" Type = ""primary"" > 编辑</Button >";
            }
            else
            {
                return @$"<Button Size=""small"" OnClick = ""()=>updateForm.ShowModal( mapper.Map<{PageData.MainViewType.Name},{PageData.UpdateViewType.Name}>(context))"" Type = ""primary"" > 编辑</Button >";
            }
        }

        public string GetMainViewCode()
        {
            if (IsTableType(PageData.MainViewType))
            {
               return @$"<AntTableView TModel=""{PageData.MainViewType.Name}""  SelectedRowChanged=""SelectData"" @ref=""table"">
<ToolbarTemplate> {GetCreateButton()} </ToolbarTemplate>
    <ActionColumnTemplate>
            <Space>
            <SpaceItem>
            <Space>

            <SpaceItem> 
                
{GetUpdateButton()}
            </SpaceItem>
            <SpaceItem> 
                <Button Size=""small"" Danger OnClick =""async ()=> {{ SelectedData = context; OpenDeleteConfirmModal();}}"" > 删除 </Button> 
            </SpaceItem>
            </Space>
            </SpaceItem>
            </Space>
    </ActionColumnTemplate>

</AntTableView>";
            }
            else
            {
                return @$"<AntTreeView TModel=""{PageData.MainViewType.Name}"" @bind-SelectedData=""SelectedData"" @ref=""table""></AntTreeView>
  @if(SelectedData != null)
    {{
        <div>
            <Button Type=""primary"" Size=""small"" OnClick=""() => createForm.ShowModal(new {PageData.CreateViewType.Name} {{ ParentId = SelectedData.ParentId }})"">添加同级</Button>
            <Button Type=""primary"" Size=""small"" OnClick=""() => createForm.ShowModal(new {PageData.CreateViewType.Name} {{ ParentId = SelectedData.Id }})"">添加下级</Button>
            <Button Type=""primary"" Size=""small"" OnClick=""() => updateForm.ShowModal(mapper.Map<{PageData.MainViewType.Name},{PageData.UpdateViewType.Name}>(SelectedData))"">编辑</Button>
            <Button Type=""danger"" Size=""small"" OnClick=""OpenDeleteConfirmModal"" Disabled=""SelectedData == null"">删除</Button>
        </div>
        

        <AntDynamicForm TModel=""{PageData.CreateViewType.Name}"" mode=""modal"" @ref=""createForm"" OnSubmit=""async () => {{await createForm.InsertAsync();await table.Refresh(); }}""></AntDynamicForm>
        <AntDynamicForm TModel=""{PageData.UpdateViewType.Name}"" mode=""modal"" @ref=""updateForm"" OnSubmit=""async () =>{{await updateForm.UpdateAsync();await table.Refresh(); }}""></AntDynamicForm>
    }}
";
            }
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
            return @$"<AntDynamicForm mode=""modal"" TModel=""{PageData.CreateViewType.Name}"" @ref=""createForm"" OnSubmit=""async () =>{{await createForm.InsertAsync();await table.Load(); }}""></AntDynamicForm>";
        }
        public bool IsCreateTabs()
        {
            if (PageData.CreateViewTabs != null)
            {
                if (PageData.CreateViewTabs.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetUpdateView()
        {
            if (PageData.UpdateViewTabs != null)
            {
                if (PageData.UpdateViewTabs.Count > 0)
                {
                    return GetUpdateTabsViewCode();
                }
            }
            return GetUpdateFormViewCode();


        }

        public bool IsUpdateTabs()
        {
            if (PageData.UpdateViewTabs != null)
            {
                if (PageData.UpdateViewTabs.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetUpdateFormViewCode()
        {
            return @$"<AntDynamicForm mode=""modal"" TModel=""{PageData.UpdateViewType.Name}"" @ref=""updateForm"" OnSubmit=""async () =>{{await updateForm.InsertAsync();await table.Load(); }}""></AntDynamicForm>";
        }
        public string GetCreateTabsViewCode()
        {
            var createTabs = PageData.CreateViewTabs;
            return @$"
@if(CreateData!=null)
{{

    <Modal Visible=""CreateData!=null"" Width=""900"" Title=""添加角色"" OnOk=""HandleOk"" OnCancel=""()=>{{CreateData = null;}}"">
        <Tabs>
            <TabPane Key=""1"">
                <Tab>基本信息</Tab>
                <ChildContent>
                    <AntDynamicForm TModel=""{PageData.CreateViewType.Name}"" Value=""CreateData""></AntDynamicForm>
                </ChildContent>
            </TabPane>
{string.Join('\n',createTabs.Select(tab=> GetSubCreateTable(tab)) ) }
           
        </Tabs>

    </Modal>
}}";
        }

public string GetSubCreateTable(TabConfig tab)
{
     return    @$"
<TabPane Key=""{tab.Title}"">
  <Tab>{tab.Title}</Tab>
<ChildContent>
{GetSubTablCreateCode(tab)}
   
</ChildContent>
</TabPane>
";
    }

        public string GetAllDetailTabCodes()
        {
            if (PageData.DetailViewTabs!=null)
            {
                if (PageData.DetailViewTabs.Count > 0)
                {
                    return string.Join('\n', PageData.DetailViewTabs.Select(tab => GetSubDetailTable(tab)));
                }
            }
            
            return "";

        }

        public string GetSubDetailTable(TabConfig tab)
        {
            
            return @$"
<TabPane Key=""{tab.Title}"">
  <Tab>{tab.Title}</Tab>
<ChildContent>

{GetSubDetailTableCode(tab)}
   
</ChildContent>
</TabPane>
";
        }

        public string GetSubDetailTableCode(TabConfig tab)
        {
            if (!IsTableType(tab.ModelType))
            {
                return @$"<AntTreeView  DataListTItem=""SelectedData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTreeView>";
            }
            else
            {
                return @$"<AntTableView  DataListTItem=""SelectedData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTableView>";
            }
        }
        public string GetSubUpdateTable(TabConfig tab)
        {
            return @$"
<TabPane Key=""{tab.Title}"">
  <Tab>{tab.Title}</Tab>
<ChildContent>
{GetSubTableUpdateCode(tab)}
   
</ChildContent>
</TabPane>
";
        }
            public string GetSubTablCreateCode(TabConfig tab)
        {
            if (!IsTableType(tab.ModelType))
            {
                return @$"<AntTreeView Checkable=""true"" @bind-CheckedNodes=""CreateData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTreeView>";
            }
            else
            {
                return @$"<AntTableView  @bind-CheckedNodes=""CreateData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTableView>";
            }
        }

        public string GetSubTableUpdateCode(TabConfig tab)
        {
            if (!IsTableType(tab.ModelType))
            {
                return @$"<AntTreeView Checkable=""true"" @bind-CheckedNodes=""EditData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTreeView>";
            }
            else
            {
                return @$"<AntTableView   @bind-CheckedNodes=""EditData.{tab.PropertyName}"" TModel=""{tab.ModelType.Name}""></AntTableView>";
            }
        }

        public bool IsTableType(Type type)
        {
            return type.GetCustomAttribute<TablePageAttribute>(true)!=null;
        }

        public string GetUpdateTabsViewCode()
        {
            var updateTabs = PageData.UpdateViewTabs;
            return @$"
@if(EditData!=null)
{{

    <Modal Visible=""EditData!=null"" Width=""900"" Title=""编辑角色"" OnOk=""HandleOk"" OnCancel=""()=>{{EditData = null;}}"">
        <Tabs>
            <TabPane Key=""1"">
                <Tab>基本信息</Tab>
                <ChildContent>
                    <AntDynamicForm TModel=""{PageData.UpdateViewType.Name}"" Value=""EditData""></AntDynamicForm>
                </ChildContent>
            </TabPane>
{string.Join('\n', updateTabs.Select(tab => GetSubUpdateTable(tab))) }
        </Tabs>

    </Modal>
}}";
        }
    }
}
