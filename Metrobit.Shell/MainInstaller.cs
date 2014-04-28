using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using com.google.bitcoin.core;
using com.google.bitcoin.@params;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Models;

namespace Metrobit.Shell
{
    public class MainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInThisApplication().BasedOn<ViewModelBase>());

            SetUpBitcoinWallet(container);
        }

        private static void SetUpBitcoinWallet(IWindsorContainer container)
        {
            //Currently hard coded to work with TestNet.
            NetworkParameters parameters = TestNet3Params.get();

            var dataDirectory = App.AppDataDirectory;

            var appKit = new MetrobitWalletAppKit(parameters, new java.io.File(dataDirectory), "metrobit");

            appKit.setAutoSave(true);
            appKit.setDownloadListener(new MetrobitDownloadListener());
            
            //Not sure if we need this yet.
            var service = appKit.startAsync();

            container.Register(Component.For<MetrobitWalletAppKit>().Instance(appKit));
        }
    }
}
