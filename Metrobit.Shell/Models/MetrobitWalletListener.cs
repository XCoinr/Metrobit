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

            Futures.addCallback(tx.getConfidence().getDepthFuture(1), new ReceivedCoinsFutureCallback(wallet,tx,prevBalance,newBalance));
        }

        public override void onCoinsSent(Wallet wallet, Transaction tx, BigInteger prevBalance, BigInteger newBalance)
        {
            log.InfoFormat("onCoinsSent: wallet: {0}, tx: {1}, prevBalance: {2}, newBalance: {3}", wallet, tx, prevBalance, newBalance);

            base.onCoinsSent(wallet, tx, prevBalance, newBalance);

            var msg = new CoinsSentMessage
            {
                Wallet = wallet,
                Transaction = tx,
                PreviousBalance = prevBalance,
                NewBalance = newBalance
            };

            Messenger.Default.Send(msg);

            Futures.addCallback(tx.getConfidence().getDepthFuture(1), new SentCoinsFutureCallback(wallet, tx, prevBalance, newBalance));
        }

        public override void onKeysAdded(Wallet wallet, List keys)
        {
            log.InfoFormat("onKeysAdded: wallet: {0}, keys: {1}", wallet, keys);

            base.onKeysAdded(wallet, keys);

            var msg = new KeysAddedMessage {Keys = keys, Wallet = wallet};
            Messenger.Default.Send(msg);
        }

        public override void onReorganize(Wallet wallet)
        {
            log.InfoFormat("onReorganize: wallet: {0}", wallet);

            base.onReorganize(wallet);

            var msg = new WalletReorganizeMessage { Wallet = wallet };
            Messenger.Default.Send(msg);
        }

        public override void onScriptsAdded(Wallet wallet, List scripts)
        {
            log.InfoFormat("onScriptsAdded: wallet: {0}, scripts: {1}", wallet, scripts);

            base.onScriptsAdded(wallet, scripts);

            var msg = new ScriptsAddedMessage { Wallet = wallet, Scripts = scripts};
            Messenger.Default.Send(msg);
        }

        public override void onTransactionConfidenceChanged(Wallet wallet, Transaction tx)
        {
            log.InfoFormat("onTransactionConfidenceChanged: wallet: {0}, tx: {1}", wallet, tx);

            base.onTransactionConfidenceChanged(wallet, tx);

            var msg = new TransactionConfidenceChangedMessage { Wallet = wallet, Transaction = tx};
            Messenger.Default.Send(msg);
        }

        public override void onWalletChanged(Wallet wallet)
        {
            log.InfoFormat("onWalletChanged: wallet: {0}", wallet);

            base.onWalletChanged(wallet);

            var msg = new WalletChangedMessage { Wallet = wallet };
            Messenger.Default.Send(msg);
            
        }
    }
}