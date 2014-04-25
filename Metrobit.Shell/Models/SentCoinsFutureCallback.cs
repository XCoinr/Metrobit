using System;
using com.google.common.util.concurrent;

namespace Metrobit.Shell.Models
{
    public class SentCoinsFutureCallback : FutureCallback
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void onSuccess(object obj)
        {
            _log.InfoFormat("onSuccess: {0}", obj);
        }

        public void onFailure(Exception t)
        {
            _log.InfoFormat("onFailuure: {0}", t);
        }
    }
}
