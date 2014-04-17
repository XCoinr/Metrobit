using System.Collections.ObjectModel;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
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
    }
}
