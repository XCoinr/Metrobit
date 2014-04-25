using System;
using com.google.bitcoin.core;
using java.util;
using Metrobit.Shell.Models.DAL;

namespace Metrobit.Shell.Utils
{
    /// <summary>
    /// A utility class to help with the inevitable, clunky bridging between C# and Java
    /// </summary>
    public static class BitcoinJExtentions
    {
        public static MbTransaction.ConfidenceTypes ToMbConfidenceTypes(
            this TransactionConfidence.ConfidenceType b4JConfidenceType)
        {
            if (b4JConfidenceType == TransactionConfidence.ConfidenceType.BUILDING)
            {
                return MbTransaction.ConfidenceTypes.Building;
            }
            if (b4JConfidenceType == TransactionConfidence.ConfidenceType.DEAD)
            {
                return MbTransaction.ConfidenceTypes.Dead;
            }
            if (b4JConfidenceType == TransactionConfidence.ConfidenceType.PENDING)
            {
                return MbTransaction.ConfidenceTypes.Pending;
            }
            if (b4JConfidenceType == TransactionConfidence.ConfidenceType.UNKNOWN)
            {
                return MbTransaction.ConfidenceTypes.Pending;
            }

            throw new ArgumentException("Unexpexted confidence type");
        }

        public static DateTime ToDateTime(this Date date)
        {
            //Returns the number of milliseconds since January 1, 1970, 00:00:00 GMT represented by this Date object.
            var javaDateTicks = date.getTime();

            var epoch = new DateTime(1970, 1, 1);

            var dateTime = epoch.AddMilliseconds(javaDateTicks);

            return dateTime;
        }
    }
}
