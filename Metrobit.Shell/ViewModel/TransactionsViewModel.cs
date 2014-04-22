using System.Collections.ObjectModel;
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
            Transactions = new ObservableCollection<Transaction>();

            var txs = _appKit.wallet().getTransactions(false).toArray();

            for (int i = 0; i < txs.GetLength(0); i++)
            {
                Transactions.Add(txs.GetValue(i) as Transaction);
            }
        }

        public ObservableCollection<Transaction> Transactions { get; private set; }
    }
}
