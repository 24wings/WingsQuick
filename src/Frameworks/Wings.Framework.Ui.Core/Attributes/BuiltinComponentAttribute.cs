namespace Wings.Framework.Ui.Core.Attributes
{
    public enum BuiltinComponentType
    {
        View,
        Field,
        Prop

    }
    public class BuiltinComponentAttribute : System.Attribute
    {
        public string ComponentName { get; set; }

        public BuiltinComponentAttribute(string componentName)
        {
            ComponentName = componentName;
        }

    }

}