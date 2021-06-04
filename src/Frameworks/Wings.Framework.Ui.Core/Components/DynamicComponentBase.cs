using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Framework.Ui.Core.Components
{
    /// <summary>
    /// 动态组件基类
    /// </summary>
    public abstract class DynamicComponentBase : ComponentBase
    {
        public abstract string Category { get; set; }

    }

}