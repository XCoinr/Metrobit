using System.Collections.ObjectModel;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
            Transactions = new ObservableCollection<MbTransaction>();

            var transactions = _appKit.wallet().getTransactions(false);

            foreach (var t in transactions.toArray())
            {
                var tx = t as Transaction;


            }

            Messenger.Default.Register<NewTransactionMessage>(this, message => Transactions.Add(message.Transaction));
        }

        public ObservableCollection<MbTransaction> Transactions { get; private set; }
    }
}
