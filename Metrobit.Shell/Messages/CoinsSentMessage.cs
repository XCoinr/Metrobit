using com.google.bitcoin.core;
using java.math;

namespace Metrobit.Shell.Messages
{
    class CoinsSentMessage
    {
        public Wallet Wallet { get; set; }
        public Transaction Transaction { get; set; }
        public BigInteger PreviousBalance { get; set; }
        public BigInteger NewBalance { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
