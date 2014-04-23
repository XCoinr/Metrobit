using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;

namespace Metrobit.Shell.Messages
{
    class WalletChangedMessage : MessageBase
    {
        public Wallet Wallet { get; set; }  
    }
}
