using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RM.DomainModel
{
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=RMContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierContactDetail> SupplierContactDetail { get; set; }
    }
}
