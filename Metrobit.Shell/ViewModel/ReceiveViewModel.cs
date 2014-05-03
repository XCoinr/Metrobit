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
        private MetrobitWalletAppKit _appKit;
        
        public ReceiveViewModel(MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
            Addresses = new ObservableCollection<MbAddress>();

            ReloadAddresses();

            Messenger.Default.Register<KeysAddedMessage>(this, message => DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                using (var ctx = new MbContext())
                {
                    var parameters = ServiceLocator.Current.GetInstance<MetrobitWalletAppKit>().@params();

                    for (var i = 0; i < message.Keys.size(); i++)
                    {
                        var key = message.Keys.get(i) as ECKey;

                        var mbAddress =
                            (from a in ctx.Addresses
                                where a.Address == new Address(parameters, key.getPubKeyHash()).toString()
                                select a).First();

                        Addresses.Add(mbAddress);
                    }
                }
            }));
        }

        private void ReloadAddresses()
        {
            Addresses.Clear();

            using (var ctx = new MbContext())
            {
                foreach (var mbAddress in ctx.Addresses)
                {
                    Addresses.Add(mbAddress);
                }
            }
        }

        public ObservableCollection<MbAddress> Addresses{ get; private set; }

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
