using System;
using com.google.bitcoin.core;
using com.google.bitcoin.jni;
using com.google.bitcoin.kits;
using com.google.bitcoin.@params;
using com.google.common.util.concurrent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private MetrobitWalletAppKit _appKit;
        private Service _service;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            NetworkParameters parameters = TestNet3Params.get();

            _appKit = new MetrobitWalletAppKit(parameters, new java.io.File("D:\\Crypto\\MetrobitData"), "metrobit");
            _service = _appKit.startAsync();
        }
    }

    public class MetrobitWalletAppKit : WalletAppKit
    {
        public MetrobitWalletAppKit(NetworkParameters @params, java.io.File directory, string filePrefix) : base(@params, directory, filePrefix)
        {
            
        }

        protected override void onSetupCompleted()
        {
            base.onSetupCompleted();

            if (wallet().getKeychainSize() < 1)
            {
                wallet().addKey(new ECKey());
            }

            wallet().addEventListener(new MetrobitWalletListener());
        }
    }

    public class MetrobitWalletListener : AbstractWalletEventListener
    {
        public override void onCoinsReceived(Wallet wallet, Transaction tx, java.math.BigInteger prevBalance, java.math.BigInteger newBalance)
        {
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
}