using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntDetailPropBase<TModel> : PropertyComponentBase<TModel>
    {

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (detailViewAttribute != null)
            {
                if (detailViewAttribute.Show == false)
                {
                }
            }

        }

    }

}