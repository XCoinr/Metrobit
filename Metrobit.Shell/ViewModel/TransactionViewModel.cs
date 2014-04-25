using System;
using System.Collections.Generic;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Models;
using Metrobit.Shell.Utils;

namespace Metrobit.Shell.ViewModel
{
    public class TransactionViewModel : ViewModelBase
    {
        private Transaction _transaction;
        private readonly MetrobitWalletAppKit _appKit;

        public TransactionViewModel(Transaction transaction, MetrobitWalletAppKit appKit)
        {
            _transaction = transaction;
            _appKit = appKit;
        }

        public TransactionConfidence.ConfidenceType ConfidenceType
        {
            get { return _transaction.getConfidence().getConfidenceType(); }
        }

        public int Depth
        {
            get { return _transaction.getConfidence().getDepthInBlocks(); }
        }

        public DateTime Timestamp
        {
            get { return _transaction.getUpdateTime().ToDateTime(); }
        }
    }
}
