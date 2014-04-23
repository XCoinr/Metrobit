using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;

namespace Metrobit.Shell.Messages
{
    public class BroadcastCompleteMessage : MessageBase
    {
        public Transaction Transaction { get; set; }
    }
}
