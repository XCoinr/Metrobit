using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;
using java.math;

namespace Metrobit.Shell.Messages
{
    public class CoinsReceivedMessage : MessageBase
    {
        public Wallet Wallet { get; set; }
        public Transaction Transaction { get; set; }
        public BigInteger PreviousBalance { get; set; }
        public BigInteger NewBalance { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
