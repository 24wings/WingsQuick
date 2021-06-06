using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;
using AntDesign;
using Wings.Examples.UseCase.Client.Services;
using Tewr.Blazor.FileReader;
using System.IO;
using Wings.Examples.UseCase.Shared.Dto;

namespace Wings.Examples.UseCase.Client.Pages
{
    public class PersonSettingBase : ComponentBase
    {
        [Inject]
        protected ResourceService resourceService { get; set; }
        [Inject]
        protected MessageService _message { get; set; }
        protected ElementReference inputTypeFileElement;
        [Inject]
        protected IFileReaderService fileReaderService { get; set; }


        public async Task ReadFile()
        {
            foreach (var file in await fileReaderService.CreateReference(inputTypeFileElement).EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();
                var buffer = new Byte[fileInfo.Size];
                // Read into buffer and act (uses less memory)
                await using (Stream stream = await file.OpenReadAsync())
                {
                    // Do (async) stuff with stream...
                    await stream.ReadAsync(buffer, 0, (int)fileInfo.Size);
                    string a = Convert.ToBase64String(buffer);
                    Console.WriteLine(a);

                    // The following will fail. Only async read is allowed.
                    //stream.Read(buffer, ...)
                }

                // Read file fully into memory and act
                //using (MemoryStream memoryStream = await file.CreateMemoryStreamAsync(4096))
                //{

                //    // Sync calls are ok once file is in memory
                //    //memoryStream.Read(buffer, ...)
                //}
            }
        }
       protected  RbacUserModel userModel = new RbacUserModel();
       protected  Form<RbacUserModel> editForm;
        protected List<ProvinceJson> provinceJsons;

        protected override async Task OnParametersSetAsync()
        {
            provinceJsons = await resourceService.loadProvinceJson();

            await refresh();
        }

        protected string _cityValue;
        protected string[] _cities = new string[0];
        protected readonly string[] _provinceData = new[] { "Zhejiang", "Jiangsu" };





        //private void OnChange(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        //{
        //    var province = provinceJsons.FirstOrDefault(p => p.name == value.AsT0);
        //    userModel.Province = province.name;
        //    _cities = province.city.Select(c => c.name).ToArray();
        //    userModel.City = null;
        //    InvokeAsync(StateHasChanged);
        //    Console.WriteLine($"selected: ${value}");
        //}

        //private void OnChangeCity(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        //{
        //    Console.WriteLine($"selected: ${value}");
        //    userModel.City = value.Value.ToString();
        //    InvokeAsync(StateHasChanged);
        //}




        public async Task OnFinish()
        {
            //var rtn = await userService.updateUser(userModel);
            //await _message.Success(rtn.msg);
            await refresh();

        }
        public async Task OnFinishFailed()
        {

        }

        public async Task refresh()
        {
            //userModel = await userService.currentUser();
            //if (!string.IsNullOrEmpty(userModel.Province))
            //{
            //    var province = provinceJsons.FirstOrDefault(p => p.name == userModel.Province);
            //    userModel.Province = province.name;
            //    _cities = province.city.Select(c => c.name).ToArray();
            //}
        }
    }
}
