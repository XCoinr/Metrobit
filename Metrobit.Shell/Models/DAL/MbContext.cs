using System.Data.Entity;

namespace Metrobit.Shell.Models.DAL
{
    public class MbContext : DbContext
    {
        public MbContext()
            :base("DataContext")
        {
            
        }

        //public DbSet<MbTransaction> Transactions { get; set; }
        public DbSet<MbAddress> Addresses { get; set; }
    }
}
