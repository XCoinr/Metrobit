using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;
using java.util;

namespace Metrobit.Shell.Messages
{
    public class KeysAddedMessage : MessageBase
    {
        public Wallet Wallet { get; set; }
        public List Keys { get; set; }
    }
}
