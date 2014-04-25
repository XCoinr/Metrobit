using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Models.DAL;

namespace Metrobit.Shell.Messages
{
    public class NewTransactionMessage : MessageBase
    {
        public Wallet Wallet { get; set; }
        public MbTransaction Transaction { get; set; }
    }
}
