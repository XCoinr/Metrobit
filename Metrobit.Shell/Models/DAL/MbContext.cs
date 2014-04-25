using System.Data.Entity;

namespace Metrobit.Shell.Models.DAL
{
    public class MbContext : DbContext
    {
        public DbSet<MbTransaction> Transactions { get; set; }
    }
}
