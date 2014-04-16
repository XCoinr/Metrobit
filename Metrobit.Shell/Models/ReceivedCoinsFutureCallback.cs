using System;
using com.google.bitcoin.core;
using com.google.common.util.concurrent;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.Models
{
    public class ReceivedCoinsFutureCallback : FutureCallback
    {
        private CoinsReceivedMessage _message;

        public ReceivedCoinsFutureCallback(Wallet wallet, Transaction tx, java.math.BigInteger prevBalance, java.math.BigInteger newBalance)
        {
            _message = new CoinsReceivedMessage
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