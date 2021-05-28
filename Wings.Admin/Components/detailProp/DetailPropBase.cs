namespace Wings.Admin.Components.detailProp
{
    public class DetailPropBase<TModel> : PropertyComponentBase<TModel>
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