using GalaSoft.MvvmLight.Messaging;
using log4net.Core;

namespace Metrobit.Shell.Messages
{
    public class Log4NetMessage : MessageBase
    {
        public LoggingEvent Event { get; set; }
    }
}
