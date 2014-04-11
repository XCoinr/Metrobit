using System.Deployment.Application;
using System.Reflection;
using GalaSoft.MvvmLight;

namespace Metrobit.Shell.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public string AboutText
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return "ClickOnce Deployed, v" + ApplicationDeployment.CurrentDeployment.CurrentVersion;
                }
                
                return "Development build. v" + Assembly.GetExecutingAssembly().GetName().Version +
                       " - NOT FOR PRODUCTION!";
            }
        }
    }
}
