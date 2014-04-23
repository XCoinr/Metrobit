using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;

namespace Metrobit.Shell.Messages
{
    public class WalletReorganizeMessage : MessageBase
    {
        public Wallet Wallet { get; set; }
    }
}
