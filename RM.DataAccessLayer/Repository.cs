using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using RM.DomainModel;

namespace RM.DataAccessLayer
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
    }

    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        DbContext _context;
        public GenericDataRepository(DbContext context)
        {
            if (context == null) { 
                throw new ArgumentNullException("context");
            }
            _context = context;
        }
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            //using (var context = new Entities()) {
                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            //}
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            //using (var context = new Entities()) {
                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            //}
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            //using (var context = new Entities()) {
                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            //}
            return item;
        }

        /* rest of code omitted */
        public virtual void Add(params T[] items)
        {
            Update(items);
        }

        public virtual void Update(params T[] items)
        {
            //using (var context = new Entities()) {
                DbSet<T> dbSet = _context.Set<T>();
                foreach (T item in items) {
                    dbSet.Add(item);
                    foreach (DbEntityEntry<IEntity> entry in _context.ChangeTracker.Entries<IEntity>()) {
                        IEntity entity = entry.Entity;
                        entry.State = GetEntityState(entity.EntityState);
                    }
                }
                //context.SaveChanges();
            //}
        }
 
        public virtual void Remove(params T[] items)
        {
            Update(items);
        }
        protected static System.Data.Entity.EntityState GetEntityState(RM.DomainModel.EntityState entityState)
        {
            switch (entityState) {
                case DomainModel.EntityState.Unchanged:
                    return System.Data.Entity.EntityState.Unchanged;
                case DomainModel.EntityState.Added:
                    return System.Data.Entity.EntityState.Added;
                case DomainModel.EntityState.Modified:
                    return System.Data.Entity.EntityState.Modified;
                case DomainModel.EntityState.Deleted:
                    return System.Data.Entity.EntityState.Deleted;
                default:
                    return System.Data.Entity.EntityState.Detached;
            }
        }
    }

    //public interface ISupplierRepository : IGenericDataRepository<Supplier>
    //{
    //}

    //public interface ISupplierContactDetailsRepository : IGenericDataRepository<SupplierContactDetail>
    //{
    //}

    //public class SupplierRepository : GenericDataRepository<Supplier>, ISupplierRepository
    //{
    //}

    //public class SupplierContactDetailsRepository : GenericDataRepository<SupplierContactDetail>, ISupplierContactDetailsRepository
    //{
    //}
}
