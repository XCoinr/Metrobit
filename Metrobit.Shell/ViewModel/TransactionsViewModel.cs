using System.Collections.ObjectModel;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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

            using (var ctx = new MbContext())
            {
                foreach (var mbTransaction in ctx.Transactions)
                {
                    Transactions.Add(mbTransaction);
                }
            }

            Messenger.Default.Register<NewTransactionMessage>(this, message => Transactions.Add(message.Transaction));
        }

        public ObservableCollection<MbTransaction> Transactions { get; private set; }
    }
}
