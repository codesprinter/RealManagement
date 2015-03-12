using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace RM.DataAccessLayer
{
    public static class DbContextExtensions
    {
        public static System.Data.Entity.Core.Objects.ObjectContext ToObjectContext(this DbContext dbContext)
        {
            return (dbContext as IObjectContextAdapter).ObjectContext;
        }
    }
}
