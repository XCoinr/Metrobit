using System.ComponentModel.DataAnnotations;

namespace Metrobit.Shell.Models.DAL
{
    public class MbAddress
    {
        [Key]
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
