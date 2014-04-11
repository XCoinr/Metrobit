using System.Windows;
using FirstFloor.ModernUI.Presentation;
using Metrobit.Shell.Properties;

namespace Metrobit.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppearanceManager.Current.ThemeSource = Settings.Default.ThemeSource;
            AppearanceManager.Current.AccentColor = Settings.Default.AccentColor;
        }
    }
}
