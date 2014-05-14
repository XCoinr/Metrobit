using System;
using System.Net.Mime;
using System.Windows;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Models;

namespace Metrobit.Shell.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private MetrobitWalletAppKit _appKit;
        public HomeViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;

            _appKit.WalletSetupComplete += InitialiseBalance;
            InitialiseBalance();

            Messenger.Default.Register<NewTransactionMessage>(this, message => InitialiseBalance());
        }

        #region Balance

        private void InitialiseBalance()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (!_appKit.IsSetupComplete)
                {
                    return;
                }

                var bal = _appKit.wallet().getBalance(Wallet.BalanceType.ESTIMATED);

                Balance = com.google.bitcoin.core.Utils.bitcoinValueToFriendlyString(bal);
            });
        }

        private string _balance = null;

        /// <summary>
        /// Gets or sets the Balance property. This observable property 
        /// indicates ....
        /// </summary>
        public string Balance
        {
            get { return _balance; }
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    RaisePropertyChanged("Balance");
                }
            }
        }

        #endregion
    }
}
