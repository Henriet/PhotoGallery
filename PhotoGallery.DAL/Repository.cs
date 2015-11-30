using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoGalery.DAL
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context = new Context();
        private bool _disposed;

        private IDbSet<T> _objectset;

        private IDbSet<T> DbSet
        {
            get { return _objectset ?? (_objectset = _context.Set<T>()); }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T Get(int id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public T Insert(T item)
        {
            try
            {
                T entity = DbSet.Add(item);
                CommitChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Update(T entity)
        {
            try
            {
                DbSet.Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;
                CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                T entity = Get(id);
                if (entity == null) return false;

                DbSet.Remove(entity);
                CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void CommitChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<T> All()
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _context.Dispose();
                    }
                }
                _disposed = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
