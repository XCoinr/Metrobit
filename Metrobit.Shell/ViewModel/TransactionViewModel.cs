using System;
using System.Collections.Generic;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Models;

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
            get
            {
                //Returns the number of milliseconds since January 1, 1970, 00:00:00 GMT represented by this Date object.
                var javaDateTicks = _transaction.getUpdateTime().getTime();

                var epoch = new DateTime(1970, 1, 1);

                var timestamp = epoch.AddMilliseconds(javaDateTicks);

                return timestamp;
            }
        }
    }
}
