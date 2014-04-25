using System;
using System.IO;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;
using Metrobit.Shell.Properties;

namespace Metrobit.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        public static string AppDataDirectory { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Metrobit");

            AppDomain.CurrentDomain.SetData("DataDirectory", AppDataDirectory);

            base.OnStartup(e);

            AppearanceManager.Current.ThemeSource = Settings.Default.ThemeSource;
            AppearanceManager.Current.AccentColor = Settings.Default.AccentColor;
        }
    }
}
