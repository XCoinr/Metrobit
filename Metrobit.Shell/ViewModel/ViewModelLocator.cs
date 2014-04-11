/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Metrobit.Shell"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Castle.Windsor;
using Castle.Windsor.Installer;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Content;
using Metrobit.Shell.Utils;
using Microsoft.Practices.ServiceLocation;

namespace Metrobit.Shell.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                var container = new WindsorContainer();
                container.Install(FromAssembly.This());

                ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
            }


            // Pre activate
            var temp = Logging;

            log.Debug("Test Debug");
            log.Info("Test Info");
            log.Warn("Test warn");
            log.Error("Test Error");
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsAppearanceViewModel SettingsAppearance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsAppearanceViewModel>();
            }
        }

        public LoggingViewModel Logging
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoggingViewModel>();
            }
        }

        public AboutViewModel About
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}