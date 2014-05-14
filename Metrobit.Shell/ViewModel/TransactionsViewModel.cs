using System.Collections.ObjectModel;
using System.Linq;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using java.util;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Models;
using Metrobit.Shell.Models.DAL;

namespace Metrobit.Shell.ViewModel
{
    public class TransactionsViewModel : ViewModelBase
    {
        private MetrobitWalletAppKit _appKit;

        public TransactionsViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
            Transactions = new ObservableCollection<TransactionViewModel>();

            var transactions = _appKit.wallet().getTransactions(false);

            foreach (var tx in transactions.toArray().Select(t => t as Transaction))
            {
                Transactions.Add(new TransactionViewModel(tx, appKit));
            }

            Messenger.Default.Register<NewTransactionMessage>(this,
                message =>
                    DispatcherHelper.CheckBeginInvokeOnUI(
                        () => Transactions.Add(new TransactionViewModel(message.Transaction, appKit))));
        }

        public ObservableCollection<TransactionViewModel> Transactions { get; private set; }
    }
}
