using System;
using System.IO;
using System.Linq;
using com.google.bitcoin.core;
using com.google.bitcoin.kits;
using Metrobit.Shell.Models.DAL;

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

            wallet().addEventListener(new MetrobitWalletListener());

            if (!File.Exists(vWalletFile.getAbsolutePath()))
            {
                wallet().removeKey(wallet().getKeys().get(0) as ECKey);
            }

            if (wallet().getKeychainSize() < 1)
            {
                wallet().addKey(new ECKey());
            }

            IsSetupComplete = true;
            _log.Info("Wallet setup complete");
            OnWalletSetupComplete();
        }

        public string GetAddressDescription(string address)
        {
            string desc = null;

            using (var ctx = new MbContext())
            {
                desc = (from a in ctx.Addresses where a.Address == address select a.Description).First();
            }

            return desc;
        }
    }
}