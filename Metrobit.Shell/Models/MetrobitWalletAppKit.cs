using System;
using com.google.bitcoin.core;
using com.google.bitcoin.kits;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.Models
{
    public class MetrobitWalletAppKit : WalletAppKit
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MetrobitWalletAppKit(NetworkParameters @params, java.io.File directory, string filePrefix) : base(@params, directory, filePrefix)
        {
            setAutoSave(true);

            Messenger.Default.Register<ShutdownNotificationMessage>(null, message =>
            {
                stopAsync();
                awaitTerminated();
            });
        }

        public event Action WalletSetupComplete;

        protected virtual void OnWalletSetupComplete()
        {
            Action handler = WalletSetupComplete;
            if (handler != null) handler();
        }

        public bool IsSetupComplete { get; private set; }

        protected override void onSetupCompleted()
        {
            base.onSetupCompleted();

            if (wallet().getKeychainSize() < 1)
            {
                wallet().addKey(new ECKey());
            }

            wallet().addEventListener(new MetrobitWalletListener());

            IsSetupComplete = true;
            _log.Info("Wallet setup complete");
            OnWalletSetupComplete();
        }
    }
}