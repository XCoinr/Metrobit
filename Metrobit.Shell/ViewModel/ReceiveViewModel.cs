using System.Collections.ObjectModel;
using System.Windows.Input;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Metrobit.Shell.Models;

namespace Metrobit.Shell.ViewModel
{
    public class ReceiveViewModel : ViewModelBase
    {
        private MetrobitWalletAppKit _appKit;
        
        public ReceiveViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
            Keys = new ObservableCollection<ECKey>();

            var keys = _appKit.wallet().getKeys();

            for (var i = 0; i < keys.size(); i++)
            {
                Keys.Add(keys.get(i) as ECKey);
            }
        }

        public ObservableCollection<ECKey> Keys { get; private set; }

        #region NewKeyCommand

        private ICommand _newKeyCommand;

        public ICommand NewKeyCommand
        {
            get { return _newKeyCommand ?? (_newKeyCommand = new RelayCommand(NewKey)); }
        }

        private void NewKey()
        {
            _appKit.wallet().addKey(new ECKey());
        }

        #endregion
    }
}
