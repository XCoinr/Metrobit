using System;
using com.google.bitcoin.core;
using com.google.common.util.concurrent;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.Models
{
    public class SentCoinsFutureCallback : FutureCallback
    {
        private CoinsSentMessage _message;

        public SentCoinsFutureCallback(Wallet wallet, Transaction tx, java.math.BigInteger prevBalance, java.math.BigInteger newBalance)
        {
            _message = new CoinsSentMessage
            {
                Wallet = wallet,
                Transaction = tx,
                PreviousBalance = prevBalance,
                NewBalance = newBalance,
                IsConfirmed = true
            };
        }

        public void onSuccess(object obj)
        {
            Messenger.Default.Send(_message);
        }

        public void onFailure(Exception t)
        {
            throw new NotImplementedException();
        }
    }
}
