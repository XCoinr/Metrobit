using System;
using System.ComponentModel.DataAnnotations;

namespace Metrobit.Shell.Models.DAL
{
    public class MbTransaction
    {
        /// <summary>
        /// Yes, I know this is vulnerable to Malleability, but as it's a remote edge case, I'm going to defer looking at a better solution.
        /// </summary>
        [Key]
        public long Hash { get; set; }

        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public long PreviousBalance { get; set; }
        public long NewBalance { get; set; }
        public ConfidenceTypes ConfidenceType { get; set; }
        public int Depth { get; set; }

        public enum ConfidenceTypes
        {
            Building,
            Dead,
            Pending,
            Unkown
        }
    }
}
