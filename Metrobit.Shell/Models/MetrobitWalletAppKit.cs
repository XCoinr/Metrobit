using com.google.bitcoin.core;
using com.google.bitcoin.kits;
using Metrobit.Shell.ViewModel;

namespace Metrobit.Shell.Models
{
    public class MetrobitWalletAppKit : WalletAppKit
    {
        public MetrobitWalletAppKit(NetworkParameters @params, java.io.File directory, string filePrefix) : base(@params, directory, filePrefix)
        {
            
        }

        protected override void onSetupCompleted()
        {
            base.onSetupCompleted();

            if (wallet().getKeychainSize() < 1)
            {
                wallet().addKey(new ECKey());
            }

            wallet().addEventListener(new MetrobitWalletListener());
        }
    }
}