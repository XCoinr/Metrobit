using System;
using com.google.bitcoin.core;
using com.google.bitcoin.kits;

namespace Metrobit.Shell.Models
{
    public sealed class MetrobitWalletAppKit : WalletAppKit
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MetrobitWalletAppKit(NetworkParameters @params, java.io.File directory, string filePrefix) : base(@params, directory, filePrefix)
        {
            
        }

        public event Action WalletSetupComplete;

        private void OnWalletSetupComplete()
        {
            var handler = WalletSetupComplete;
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