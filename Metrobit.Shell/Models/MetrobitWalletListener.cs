using com.google.bitcoin.core;
using com.google.common.util.concurrent;
using GalaSoft.MvvmLight.Messaging;
using java.math;
using java.util;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.Models
{
    public class MetrobitWalletListener : AbstractWalletEventListener
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void onCoinsReceived(Wallet wallet, Transaction tx, java.math.BigInteger prevBalance, java.math.BigInteger newBalance)
        {
            log.InfoFormat("onCoinsReceived: wallet: {0}, tx: {1}, prevBalance: {2}, newBalance: {3}", wallet, tx, prevBalance, newBalance);

            base.onCoinsReceived(wallet, tx, prevBalance, newBalance);

            var msg = new CoinsReceivedMessage
            {
                Wallet = wallet,
                Transaction = tx,
                PreviousBalance = prevBalance,
                NewBalance = newBalance
            };

            Messenger.Default.Send(msg);

            Futures.addCallback(tx.getConfidence().getDepthFuture(3), new ReceivedCoinsFutureCallback(wallet,tx,prevBalance,newBalance));
        }

        public override void onCoinsSent(Wallet wallet, Transaction tx, BigInteger prevBalance, BigInteger newBalance)
        {
            log.InfoFormat("onCoinsReceived: wallet: {0}, tx: {1}, prevBalance: {2}, newBalance: {3}", wallet, tx, prevBalance, newBalance);

            base.onCoinsSent(wallet, tx, prevBalance, newBalance);
        }

        public override void onKeysAdded(Wallet wallet, List keys)
        {
            base.onKeysAdded(wallet, keys);
        }

        public override void onReorganize(Wallet wallet)
        {
            base.onReorganize(wallet);
        }

        public override void onScriptsAdded(Wallet wallet, List scripts)
        {
            base.onScriptsAdded(wallet, scripts);
        }

        public override void onTransactionConfidenceChanged(Wallet wallet, Transaction tx)
        {
            base.onTransactionConfidenceChanged(wallet, tx);
        }

        public override void onWalletChanged(Wallet wallet)
        {
            base.onWalletChanged(wallet);
        }
    }
}