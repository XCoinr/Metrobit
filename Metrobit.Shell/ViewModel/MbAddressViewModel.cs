using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrobit.Shell.Models.DAL;

namespace Metrobit.Shell.ViewModel
{
    public class MbAddressViewModel
    {
        private readonly MbAddress _mbAddress;

        public MbAddressViewModel(MbAddress mbAddress)
        {
            _mbAddress = mbAddress;
        }
    }
}
