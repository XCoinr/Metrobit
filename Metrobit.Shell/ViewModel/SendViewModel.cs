using System.ComponentModel;
using System.Windows.Input;
using com.google.bitcoin.utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using java.math;
using Metrobit.Shell.Models;

namespace Metrobit.Shell.ViewModel
{
    public class SendViewModel : ViewModelBase, IDataErrorInfo
    {
        private MetrobitWalletAppKit _appKit;

        public SendViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
        }

        #region Amount

        private string _amount = null;

        /// <summary>
        /// Gets or sets the Amount property. This observable property 
        /// indicates ....
        /// </summary>
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged(() => Amount);
                }
            }
        }

        #endregion

        #region RecipientAddress

        private string _recipientAddress = null;

        /// <summary>
        /// Gets or sets the RecipientAddress property. This observable property 
        /// indicates ....
        /// </summary>
        public string RecipientAddress
        {
            get { return _recipientAddress; }
            set
            {
                if (_recipientAddress != value)
                {
                    _recipientAddress = value;
                    RaisePropertyChanged(() => RecipientAddress);
                }
            }
        }

        #endregion

        #region SendCommand

        private ICommand _sendCommand;

        public ICommand SendCommand
        {
            get { return _sendCommand ?? (_sendCommand = new RelayCommand(Send, CanSend)); }
        }

        private void Send()
        {
            var sendResult = _appKit.wallet().sendCoins(_appKit.peerGroup(), _recipientAddress, _amount);
            
            sendResult.broadcastComplete.addListener(new BroadcastCompleteListener(sendResult), new Threading.UserThread());
        }

        private bool CanSend()
        {
            return _amount.intValue() > 0 && _recipientAddress != null;
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                string error;

                if (columnName == "Amount")
                {
                    BigInteger.valueOf()
                }
            }
        }

        public string Error { get; private set; }
    }
}
