using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Models;
using Metrobit.Shell.Models.DAL;
using Microsoft.Practices.ServiceLocation;

namespace Metrobit.Shell.ViewModel
{
    public class ReceiveViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private MetrobitWalletAppKit _appKit;
        
        public ReceiveViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
            Addresses = new ObservableCollection<MbAddressViewModel>();

            ReloadAddresses();

            Messenger.Default.Register<KeysAddedMessage>(this, message => DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                for (var i = 0; i < message.Keys.size(); i++)
                {
                    var key = message.Keys.get(i) as ECKey;
                    Addresses.Add(new MbAddressViewModel(key, appKit));
                }
            }));
        }

        private void ReloadAddresses()
        {
            _log.Info("Reloading addresses");

            Addresses.Clear();

            var keys = _appKit.wallet().getKeys();

            for (int i = 0; i < keys.size(); i++)
            {
                var key = keys.get(i) as ECKey;
                Addresses.Add(new MbAddressViewModel(key, _appKit));
            }

            _log.InfoFormat("Found {0} addresses", Addresses.Count);
        }

        public ObservableCollection<MbAddressViewModel> Addresses{ get; private set; }

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

        #region RescanWalletCommand

        private ICommand _rescanWalletCommand;

        public ICommand RescanWalletCommand
        {
            get { return _rescanWalletCommand ?? (_rescanWalletCommand = new RelayCommand(RescanWallet)); }
        }

        private void RescanWallet()
        {
            var parameters = ServiceLocator.Current.GetInstance<MetrobitWalletAppKit>().@params();

            using (var ctx = new MbContext())
            {
                var keys = _appKit.wallet().getKeys();

                for (int i = 0; i < keys.size(); i++)
                {
                    var key = keys.get(i) as ECKey;
                    string textAddress = new Address(parameters, key.getPubKeyHash()).toString();

                    //Check to see if the address is already in the database.
                    var mbAddress = (from a in ctx.Addresses where a.Address == textAddress select a).FirstOrDefault();

                    if (mbAddress != null) continue;

                    var newAddress = new MbAddress
                    {
                        Address = new Address(parameters, key.getPubKeyHash()).toString()
                    };

                    ctx.Addresses.Add(newAddress);
                }

                ctx.SaveChanges();
            }

            ReloadAddresses();
        }

        #endregion
    }
}
