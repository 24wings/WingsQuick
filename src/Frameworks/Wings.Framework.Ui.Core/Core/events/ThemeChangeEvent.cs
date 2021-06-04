namespace Wings.Framework.Ui.Core
{
    public class ThemeChangeEvent
    {
        public static ThemeChangeEvent themeChangeEvent { get; set; } = new ThemeChangeEvent();
        public event HandleThemeChange OnThemeChange;
        public delegate void HandleThemeChange();
        public void RefreshTheme()
        {
            OnThemeChange();
        }
    }

}