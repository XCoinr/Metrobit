using com.google.bitcoin.core;
using GalaSoft.MvvmLight.Messaging;
using java.lang;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.Models
{
    public class BroadcastCompleteListener : Runnable
    {
        private Wallet.SendResult _sendResult;

        public BroadcastCompleteListener(Wallet.SendResult sendResult)
        {
            _sendResult = sendResult;
        }

        public void run()
        {
            var msg = new BroadcastCompleteMessage {Transaction = _sendResult.tx};
            Messenger.Default.Send(msg);
        }
    }
}
