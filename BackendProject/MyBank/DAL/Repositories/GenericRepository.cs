using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.DBCore;
using System.Reflection;

namespace DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetSingleBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Edit(T entity);
        void Save();
    }
    
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public CoreDBContext _context;
        public GenericRepository(CoreDBContext context)
        {
            this._context = context;
            this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            T entity = _context.Set<T>().Where(predicate).FirstOrDefault();
            return entity;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}