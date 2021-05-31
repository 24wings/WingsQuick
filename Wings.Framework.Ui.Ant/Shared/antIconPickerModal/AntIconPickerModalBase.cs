using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Wings.Framework.Ui.Ant.Components
{

    /// <summary>
    /// 图标选择模态框
    /// </summary>
    public class AntIconPickerModalBase : ComponentBase
    {
        protected string keywords { get; set; }
        protected bool _visible { get; set; } = true;
        protected string SelectedIcon { get; set; }
        [Parameter]
        public EventCallback<string> OnOk { get; set; }
        protected List<string> IconList { get; set; } = new List<string>(){
           "search","edit","ant-input-search-icon","step-backward","step-forward","fast-backward","fast-forward",
           "shrink","arrows-alt","down","up","left","right","caret-up","caret-down","caret-left","caret-right","up-circle","down-circle",
           "left-circle","right-circle","double-right","double-left","vertical-left","vertical-right","vertical-align-top",
           "vertical-align-middle","vertical-align-bottom","forward","backward","rollback","enter","retweet","swap","swap-left",
           "swap-right","arrow-up","arrow-down","arrow-left","arrow-right","play-circle","up-square","down-square","left-square",
           "right-square","login","logout","menu-fold","menu-unfold","border-bottom","border-horizontal","border-inner","border-outer",
           "border-left","border-right","border-top","border-verticle","pic-center","pic-left","pic-right","radius-bottomleft","radius-bottomright",
           "radius-upleft","radius-upright","fullscreen","fullscreen-exit","question","question-circle","plus","plus-circle","pause","pause-circle",
           "minus","minus-circle","plus-square","minus-square","info","info-circle","exclamation","exclamation-circle","close","close-circle",
           "close-square","check","check-circle","check-square","clock-circle","warning","issues-close","stop","form","copy","scissor","delete",
           "snippets","diff","highlight","align-center","align-left","align-right","bg-colors","bold","italic","underline","strikethrough","redo",
           "undo","zoom-in","zoom-out","font-colors","font-size","line-height","dash","small-dash","sort-ascending","sort-descending","drag","ordered-list",
           "unordered-list","radius-setting","column-width","area-chart","pie-chart","bar-chart","dot-chart","line-chart","radar-chart","heat-map","fall",
           "rise","stock","box-plot","fund","sliders","android","apple","windows","ie","chrome","github","aliwangwang","dingding","weibo-square",
           "weibo-circle","taobao-circle","html5","weibo","twitter","wechat","youtube","alipay-circle","taobao","skype","qq","medium-workmark","gitlab",
           "medium","linkedin","google-plus","dropbox","facebook","codepen","code-sandbox","amazon","google","codepen-circle","alipay","ant-design",
           "ant-cloud","aliyun","zhihu","slack","slack-square","behance","behance-square","dribbble","dribbble-square","instagram","yuque","alibaba","yahoo",
           "reddit","sketch","account-book","aim","alert","apartment","api","appstore","appstore-add","audio","audio-muted","audit","bank","barcode","bars",
           "bell","block","book","border","borderless-table","branches","bug","build","bulb","calculator","calendar","camera","car","carry-out","ci",
           "ci-circle","clear","cloud","cloud-download","cloud-server","cloud-sync","cloud-upload","cluster","code","coffee","column-height","comment",
           "compass","compress","console-sql","contacts","container","control","copyright","copyright-circle","credit-card","crown","customer-service",
           "dashboard","database","delete-column","delete-row","delivered-procedure","deployment-unit","desktop","dingtalk","disconnect","dislike","dollar",
           "dollar-circle","download","ellipsis","environment","euro","euro-circle","exception","expand","expand-alt","experiment","export","eye",
           "eye-invisible","field-binary","field-number","field-string","field-time","file","file-add","file-done","file-excel","file-exclamation",
           "file-gif","file-image","file-jpg","file-markdown","file-pdf","file-ppt","file-protect","file-search","file-sync","file-text","file-unknown",
           "file-word","file-zip","filter","fire","flag","folder","folder-add","folder-open","folder-view","fork","format-painter","frown","function",
           "fund-projection-screen","fund-view","funnel-plot","gateway","gif","gift","global","gold","group","hdd","heart","history","home","hourglass",
           "idcard","import","inbox","insert-row-above","insert-row-below","insert-row-left","insert-row-right","insurance","interaction","key","laptop",
           "layout","like","line","link","loading","loading-3-quarters","lock","mac-command","mail","man","medicine-box","meh","menu","merge-cells","message",
           "mobile","money-collect","monitor","more","node-collapse","node-expand","node-index","notification","number","one-to-one","paper-clip","partition",
           "pay-circle","percentage","phone","picture","play-square","pound","pound-circle","poweroff","printer","profile","project","property-safety",
           "pull-request","pushpin","qrcode","read","reconciliation","red-envelope","reload","rest","robot","rocket","rotate-left","rotate-right","safety",
           "safety-certificate","save","scan","schedule","security-scan","select","send","setting","shake","share-alt","shop","shopping","shopping-cart",
           "sisternode","skin","smile","solution","sound","split-cells","star","subnode","switcher","sync","table","tablet","tag","tags","team","thunderbolt",
           "to-top","tool","trademark","trademark-circle","transaction","translation","trophy","ungroup","unlock","upload","usb","user","user-add",
           "user-delete","user-switch","usergroup-add","usergroup-delete","verified","video-camera","video-camera-add","wallet","whats-app","wifi","woman",
           "ode-box-expand-trigger","ode-box-code-copy","ode-box-code-action","icon-javascript","icon-java","icon-shoppingcart","icon-python","icon-tuichu",
           "icon-facebook","icon-twitter","ooter-nav-icon-before","ooter-nav-icon-after"
        };
        protected List<string> DisplayIconList { get; set; } = new List<string> { };
        protected void SearchIcon()
        {
            if (string.IsNullOrEmpty(keywords))
            {
                DisplayIconList = IconList.ToList();
            }
            else
            {
                DisplayIconList = IconList.Where(icon => icon.Contains(keywords)).ToList();
            }



        }

        protected void HandleOk(MouseEventArgs e)
        {
            Console.WriteLine(e);
            OnOk.InvokeAsync(SelectedIcon);
            _visible = false;
        }

        protected void HandleCancel(MouseEventArgs e)
        {
            Console.WriteLine(e);
            _visible = false;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            SearchIcon();
        }
    }

}