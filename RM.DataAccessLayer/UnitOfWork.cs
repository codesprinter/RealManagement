using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RM.DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericDataRepository<T> RepositoryFor<T>() where T : class;
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }

        public IGenericDataRepository<T> RepositoryFor<T>() where T : class
        {
            return new GenericDataRepository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
