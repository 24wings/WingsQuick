using Wings.Framework.Shared;

namespace Wings.Framework.Shared
{
    public class PropConfig
    {
        public ComponentPair PropDateComponent { get; set; }
        public ComponentPair PropBoolComponent { get; set; }
        public ComponentPair PropNumberComponent { get; set; }
        public ComponentPair PropTreeViewComponent { get; set; }
        public ComponentPair PropStringComponent { get; set; }
    }
    public class FieldConfig
    {
        public ComponentPair FieldStringComponent { get; set; }
        public ComponentPair FieldDateComponent { get; set; }
        public ComponentPair FieldBoolComponent { get; set; }
        public ComponentPair FieldNumberComponent { get; set; }
        public ComponentPair FieldTreeViewComponent { get; set; }
    }
    public interface IThemeConfig
    {
        string ThemeName { get; set; }
      

        void UseCurrentTheme();
    }

}