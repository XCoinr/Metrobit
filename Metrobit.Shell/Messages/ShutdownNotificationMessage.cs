using GalaSoft.MvvmLight.Messaging;

namespace Metrobit.Shell.Messages
{
    public class ShutdownNotificationMessage : MessageBase
    {
        public void Cancel()
        {
            IsCancelled = true;
        }

        public bool IsCancelled { get; private set; }
    }
}
