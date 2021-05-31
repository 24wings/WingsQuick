using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Wings.Admin.Services;
using System.Text.Json;
using System.Threading.Tasks;
using Wings.Shared.Dto;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Examples.UseCase.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class Pane
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public List<MenuData> MenuSegments { get; set; }
    }

    public class ReuseTabsBase : ComponentBase
    {
        protected Tabs tabs { get; set; }
        public List<Pane> PaneList { get; set; } = new List<Pane>();
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected MenuService menuService { get; set; }

        protected bool showCounter { get; set; } = true;



        public void AddPanel(string link)
        {

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NavigationManager.LocationChanged += HandleLocationChanged;

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                await AddPaneByLink(NavigationManager.Uri);

            }
        }
        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {

            AddPaneByLink(e.Location);

        }

        public async Task AddPaneByLink(string link)
        {
            var dynamicComponentRegexp = @"/page/[\S]*";
            if (PaneList.Find(pane => pane.Key == link) != null)
            {
                return;
            }
            if (Regex.IsMatch(link, dynamicComponentRegexp))
            {
                var dynamicComponentClass = Regex.Match(link, dynamicComponentRegexp);
                var panelClassName = dynamicComponentClass.Value.Substring(6);
                var menu = await menuService.GetMenuDataByLink(link);
                var menuSegments = await menuService.GetMenuDataSegmentsByLink(menu);
                Console.WriteLine(JsonSerializer.Serialize(menuSegments));
                PaneList.Add(new Pane { Link = panelClassName, Title = menu.Label, Key = link, MenuSegments = menuSegments });
                tabs.ActiveKey = link;
                StateHasChanged();
            }
            else
            {
                showCounter = true;
            }
        }



    }
}