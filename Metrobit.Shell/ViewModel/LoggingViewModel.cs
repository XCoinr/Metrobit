using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using log4net.Core;
using Metrobit.Shell.Messages;

namespace Metrobit.Shell.ViewModel
{
    public class LoggingViewModel : ViewModelBase
    {
        public LoggingViewModel()
        {
            LoggingEvents = new ObservableCollection<LoggingEvent>();

            Messenger.Default.Register<Log4NetMessage>(this,
                message => DispatcherHelper.CheckBeginInvokeOnUI(() => ProcessMessage(message)));
        }

        private void ProcessMessage(Log4NetMessage message)
        {
            bool accepted;

            if (message.Event.Level == Level.Debug)
                accepted = ShowDebugMessages;
            else if (message.Event.Level == Level.Info)
                accepted = ShowInfoMessages;
            else
                accepted = true;

            if(accepted) LoggingEvents.Insert(0, message.Event);

            if (LoggingEvents.Count > 10000) LoggingEvents.Remove(LoggingEvents.Last());
        }

        public ObservableCollection<LoggingEvent> LoggingEvents { get; private set; }

        #region ShowDebugMessages

        private bool _showDebugMessages = false;

        /// <summary>
        /// Gets or sets the ShowDebugMessages property. This observable property 
        /// indicates ....
        /// </summary>
        public bool ShowDebugMessages
        {
            get { return _showDebugMessages; }
            set
            {
                if (_showDebugMessages != value)
                {
                    _showDebugMessages = value;
                    RaisePropertyChanged(() => ShowDebugMessages);
                }
            }
        }

        #endregion

        #region ShowInfoMessages

        private bool _showInfoMessages = true;

        /// <summary>
        /// Gets or sets the ShowInfoMessages property. This observable property 
        /// indicates ....
        /// </summary>
        public bool ShowInfoMessages
        {
            get { return _showInfoMessages; }
            set
            {
                if (_showInfoMessages != value)
                {
                    _showInfoMessages = value;
                    RaisePropertyChanged(() => ShowInfoMessages);
                }
            }
        }

        #endregion
    }
}
