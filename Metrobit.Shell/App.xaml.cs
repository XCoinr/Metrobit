using System;
using System.IO;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using java.util;
using java.util.concurrent;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Models;
using Metrobit.Shell.Properties;
using Microsoft.Practices.ServiceLocation;

namespace Metrobit.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

            Messenger.Default.Register<ShutdownNotificationMessage>(this, message =>
            {
                var appKit = ServiceLocator.Current.GetInstance<MetrobitWalletAppKit>();
                appKit.stopAsync();

                try
                {
                    appKit.awaitTerminated(1, TimeUnit.SECONDS);
                }
                catch (Exception exception)
                {
                    _log.Error("Failed to shutdown wallet gracefully.", exception);
                }
                finally
                {
                    java.lang.System.exit(0);
                    DispatcherHelper.CheckBeginInvokeOnUI(Shutdown);    
                }
            });
        }
    }
}
