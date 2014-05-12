using System.Linq;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using Metrobit.Shell.Models;
using Metrobit.Shell.Models.DAL;
using Microsoft.Practices.ServiceLocation;

namespace Metrobit.Shell.ViewModel
{
    public class MbAddressViewModel : ViewModelBase
    {
        private readonly MetrobitWalletAppKit _appKit;
        private MbAddress _mbAddress;

        public MbAddressViewModel(ECKey key, MetrobitWalletAppKit appKit)
        {
            _appKit = appKit;
            var parameters = _appKit.@params();
            var address = new Address(parameters, key.getPubKeyHash()).toString();

            using (var ctx = new MbContext())
            {
                _mbAddress = (from a in ctx.Addresses where a.Address == address select a).FirstOrDefault();

                if (_mbAddress == null)
                {
                    _mbAddress = new MbAddress {Address = address};
                    ctx.Addresses.Add(_mbAddress);
                    ctx.SaveChanges();
                }
            }
        }

        public string Address
        {
            get { return _mbAddress.Address; }
        }

        public string Description
        {
            get { return _mbAddress.Address; }
            set
            {
                if (_mbAddress.Description != value)
                {
                    _mbAddress.Description = value;

                    using (var ctx = new MbContext())
                    {
                        var mbAddress = (from a in ctx.Addresses where a.Address == _mbAddress.Address select a).FirstOrDefault();
                        mbAddress.Description = value;

                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}
