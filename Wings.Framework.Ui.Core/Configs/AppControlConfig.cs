namespace Wings.Framework.Ui.Core.Configs
{
    public class BasicFieldControls
    {
        public string StringControl { get; set; }
        public string NumberControl { get; set; }
    }

    public class PropDefaultControl : BasicFieldControls
    {

    }

    public class FieldDefaultControl : BasicFieldControls
    {

    }

    public static class AppControlConfig
    {
        public static PropDefaultControl propDefaultControl { get; set; } = new PropDefaultControl()
        {
            StringControl = "Wings.Framework.Ui.Ant.Component.AntPropString",
        };

        public static FieldDefaultControl fieldDefaultControl { get; set; } = new FieldDefaultControl();
    }

}