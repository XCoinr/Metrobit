using System;
using System.Linq;
using com.google.bitcoin.core;
using com.google.common.util.concurrent;
using GalaSoft.MvvmLight.Messaging;
using java.math;
using java.util;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Models.DAL;
using Metrobit.Shell.Utils;
using Microsoft.Practices.ServiceLocation;

namespace Metrobit.Shell.Models
{
    public class MetrobitWalletListener : AbstractWalletEventListener
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void onCoinsReceived(Wallet wallet, Transaction tx, java.math.BigInteger prevBalance, java.math.BigInteger newBalance)
        {
            _log.InfoFormat("onCoinsReceived: wallet: {0}, tx: {1}, prevBalance: {2}, newBalance: {3}", wallet, tx, prevBalance, newBalance);

            base.onCoinsReceived(wallet, tx, prevBalance, newBalance);

            var newTx = new MbTransaction
                {
                    Description = "Coins received",
                    ConfidenceType = tx.getConfidence().getConfidenceType().ToMbConfidenceTypes(),
                    Depth = tx.getConfidence().getDepthInBlocks(),
                    Hash = tx.getHash().toBigInteger().longValue(),
                    Timestamp = tx.getUpdateTime().ToDateTime(),
                    NewBalance = newBalance.longValue(),
                    PreviousBalance = prevBalance.longValue()
                };

            using (var ctx = new MbContext())
            {
                ctx.Transactions.Add(newTx);
                ctx.SaveChanges();
            }

            var msg = new NewTransactionMessage
            {
                Wallet = wallet,
                Transaction = newTx
            };

            Messenger.Default.Send(msg);

            Futures.addCallback(tx.getConfidence().getDepthFuture(1), new ReceivedCoinsFutureCallback());
        }

        public override void onCoinsSent(Wallet wallet, Transaction tx, BigInteger prevBalance, BigInteger newBalance)
        {
            _log.InfoFormat("onCoinsSent: wallet: {0}, tx: {1}, prevBalance: {2}, newBalance: {3}", wallet, tx, prevBalance, newBalance);

            base.onCoinsSent(wallet, tx, prevBalance, newBalance);

            var newTx = new MbTransaction
            {
                Description = "Coins sent",
                ConfidenceType = tx.getConfidence().getConfidenceType().ToMbConfidenceTypes(),
                Depth = tx.getConfidence().getDepthInBlocks(),
                Hash = tx.getHash().toBigInteger().longValue(),
                Timestamp = tx.getUpdateTime().ToDateTime(),
                NewBalance = newBalance.longValue(),
                PreviousBalance = prevBalance.longValue()
            };

            using (var ctx = new MbContext())
            {
                ctx.Transactions.Add(newTx);
                ctx.SaveChanges();
            }

            var msg = new NewTransactionMessage
            {
                Wallet = wallet,
                Transaction = newTx
            };
            
            Messenger.Default.Send(msg);

            Futures.addCallback(tx.getConfidence().getDepthFuture(1), new SentCoinsFutureCallback());
        }

        public override void onKeysAdded(Wallet wallet, List keys)
        {
            _log.InfoFormat("onKeysAdded: wallet: {0}, keys: {1}", wallet, keys);

            base.onKeysAdded(wallet, keys);

            using (var ctx = new MbContext())
            {
                var parameters = ServiceLocator.Current.GetInstance<MetrobitWalletAppKit>().@params();

                for (int i = 0; i < keys.size(); i++)
                {
                    var key = keys.get(i) as ECKey;

                    var newAddress = new MbAddress
                    {
                        Address = new Address(parameters, key.getPubKeyHash()).toString()
                    };

                    ctx.Addresses.Add(newAddress);
                }

                ctx.SaveChanges();
            }

            var msg = new KeysAddedMessage {Keys = keys, Wallet = wallet};
            Messenger.Default.Send(msg);
        }

        public override void onReorganize(Wallet wallet)
        {
            _log.InfoFormat("onReorganize: wallet: {0}", wallet);

            base.onReorganize(wallet);

            var msg = new WalletReorganizeMessage { Wallet = wallet };
            Messenger.Default.Send(msg);
        }

        public override void onScriptsAdded(Wallet wallet, List scripts)
        {
            _log.InfoFormat("onScriptsAdded: wallet: {0}, scripts: {1}", wallet, scripts);

            base.onScriptsAdded(wallet, scripts);

            var msg = new ScriptsAddedMessage { Wallet = wallet, Scripts = scripts};
            Messenger.Default.Send(msg);
        }

        public override void onTransactionConfidenceChanged(Wallet wallet, Transaction tx)
        {
            _log.InfoFormat("onTransactionConfidenceChanged: wallet: {0}, tx: {1}", wallet, tx);

            base.onTransactionConfidenceChanged(wallet, tx);

            using (var ctx = new MbContext())
            {
                var transaction =
                    (from t in ctx.Transactions where t.Hash == tx.getHash().toBigInteger().longValue() select t)
                        .FirstOrDefault();

                if (transaction == null)
                {
                    var error = "Update received for unknown transaction";
                    _log.Error(error);
                    throw new InvalidOperationException(error);
                }

                transaction.Depth = tx.getConfidence().getDepthInBlocks();
                transaction.ConfidenceType = tx.getConfidence().getConfidenceType().ToMbConfidenceTypes();

                ctx.SaveChanges();

                var msg = new TransactionConfidenceChangedMessage { Wallet = wallet, Transaction = transaction };
                Messenger.Default.Send(msg);
            }
        }

        public override void onWalletChanged(Wallet wallet)
        {
            _log.InfoFormat("onWalletChanged: wallet: {0}", wallet);

            base.onWalletChanged(wallet);

            var msg = new WalletChangedMessage { Wallet = wallet };
            Messenger.Default.Send(msg);
        }
    }
}