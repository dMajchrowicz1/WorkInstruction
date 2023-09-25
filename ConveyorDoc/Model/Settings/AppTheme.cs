using ModernWpf;
using System.Collections.Generic;


namespace ConveyorDoc.Model.Settings
{
    public class AppThemes : List<AppTheme>
    {
        public AppThemes()
        {
            Add(AppTheme.Light);
            Add(AppTheme.Dark);
        }
    }

    public class AppTheme
    {

        public static AppTheme Light { get; } = new AppTheme(Resources.Properties.Resources.Light, ApplicationTheme.Light);
        public static AppTheme Dark { get; } = new AppTheme(Resources.Properties.Resources.Dark, ApplicationTheme.Dark);

        private AppTheme(string name, ApplicationTheme value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public ApplicationTheme Value { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
