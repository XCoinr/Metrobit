using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Models;

namespace Metrobit.Shell.ViewModel
{
    public class TransactionsViewModel : ViewModelBase
    {
        private MetrobitWalletAppKit _appKit;

        public TransactionsViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;

            var txs = _appKit.wallet().getTransactions(false);


        }
    }
}
