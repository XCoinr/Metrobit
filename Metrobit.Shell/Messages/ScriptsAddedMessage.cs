using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;
using java.util;

namespace Metrobit.Shell.Messages
{
    public class ScriptsAddedMessage :MessageBase
    {
        public Wallet Wallet { get; set; }
        public List Scripts { get; set; }
    }
}
