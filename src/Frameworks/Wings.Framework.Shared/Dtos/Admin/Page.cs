using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Framework.Shared.Dtos.Admin
{

    public class PageData
    {
        public string PageLink { get; set; }
        public string PageTitle { get; set; }
        public string MainViewConfig { get; set; }
        public Type MainViewType { get; set; }
        public Type CreateViewType { get; set; }
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


    }



    public abstract class PageDesign
    {
        public PageData PageData { get; set; } = new PageData();

        public abstract PageData Design();
     

       

        public PageDesign SetPageTitle(string title) {

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
        public PageDesign SetDetailViewTabs(params TabConfig[] tabs)
        {
            PageData.DetailViewTabs = tabs.ToList();
            return this;
        }
        public TabConfig OneToOne<TModel>()
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.OneToOne };
        }
        public TabConfig JoinMany<TModel>()
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.JoinMany };
        }

        public TabConfig Self<TModel>()
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.Self };
        }
        public TabConfig ManyToMany<TModel>()
        {
            return new TabConfig { ModelType = typeof(TModel), TabRelation = TabRelation.ManyToMany };
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
