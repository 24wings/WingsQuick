using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Framework.Shared.Dtos.Admin
{
    public enum MainViewName
    {
        AntTableView,
        AntTreeView
    }
    public class PageData
    {
        public MainViewName MainViewName { get; set; }
        public string PageLink { get; set; }
        public string PageTitle { get; set; }
        public string MainViewConfig { get; set; }
        public Type MainViewType { get; set; }
        public Type CreateViewType { get; set; }
        public Type DetailViewType { get; set; }
        public List<TabConfig> CreateViewTabs { get; set; }
        public List<TabConfig> UpdateViewTabs { get; set; }
        public List<TabConfig> DetailViewTabs { get; set; }

        public Type UpdateViewType { get; set; }


    }
    public enum TabRelation
    {
        Self,
        OneToOne,
        ManyToMany,
        JoinOne,
        JoinMany

    }
    public class TabConfig
    {
        public Type ModelType { get; set; }
        public TabRelation TabRelation { get; set; }

        public string PropertyName { get; set; }
        public string Title { get; set; }


    }



    public abstract class PageDesign
    {
        public PageData PageData { get; set; } = new PageData() { MainViewName = MainViewName.AntTableView };

        public abstract PageData Design();

        public PageDesign SetMainViewName(MainViewName mainViewName)
        {
            PageData.MainViewName = mainViewName;
            return this;
        }




        public PageDesign SetPageTitle(string title)
        {

            PageData.PageTitle = title;
            return this;
        }

        public PageDesign SetMainView<TMainView>()
        {

            PageData.MainViewType = typeof(TMainView);
            return this;
        }

        public PageDesign SetCreateViewTabs(params TabConfig[] tabs)
        {
            PageData.CreateViewTabs = tabs.ToList();
            return this;
        }
        public PageDesign SetCreateViewType<TView>()
        {
            PageData.CreateViewType = typeof(TView);
            return this;
        }
        public PageDesign SetUpdateViewType<TView>()
        {
            PageData.UpdateViewType = typeof(TView);
            return this;
        }
        public PageDesign SetUpdateViewTabs(params TabConfig[] tabs)
        {
            PageData.UpdateViewTabs = tabs.ToList();
            return this;
        }
        public PageDesign SetDetailViewType<TView>()
        {
            PageData.DetailViewType= typeof(TView);
            return this;
        }
        public PageDesign SetDetailViewTabs(params TabConfig[] tabs)
        {
            PageData.DetailViewTabs = tabs.ToList();
            return this;
        }
        public TabConfig OneToOne<TModel>(string ProppertyName,string Title)
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.OneToOne, PropertyName = ProppertyName , Title = Title };
        }
        public TabConfig JoinMany<TModel>(string ProppertyName,string Title)
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.JoinMany, PropertyName = ProppertyName , Title = Title };
        }

        public TabConfig Self<TModel>(string ProppertyName,string Title)
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.Self, PropertyName = ProppertyName, Title = Title };
        }
        public TabConfig ManyToMany<TModel>(string ProppertyName,string Title)
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.ManyToMany, PropertyName = ProppertyName ,Title=Title};
        }

        public PageDesign SetPageLink(string url)
        {
            PageData.PageLink = url;
            return this;
        }

        public PageData Commit()
        {
            return PageData;
        }

    }


}
