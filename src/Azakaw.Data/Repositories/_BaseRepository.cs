using System;
using System.Linq;
using System.Linq.Expressions;
using Azakaw.Data.Context;
using Azakaw.Domain.Models;
using Azakaw.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Azakaw.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext DataContext;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DataContext dataContext)
        {
            dataContext.Database.EnsureCreated();

            DataContext = dataContext;
            DbSet = DataContext.Set<TEntity>();
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> GetAllWithInclude<TProperty>(
            params Expression<Func<TEntity, TProperty>>[] includePaths)
        {
            var query = DbSet.AsNoTracking();
            foreach (var includePath in includePaths)
            {
                query = query.Include(includePath);
            }

            return query;
        }

        public IQueryable<TEntity> GetAllWithInclude(params string[] includes)
        {
            var query = DbSet.AsNoTracking();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAllWithInclude<TProperty>(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, TProperty>>[] includePaths)
        {
            var query = DbSet.Where(predicate);
            foreach (var includePath in includePaths)
            {
                query = query.Include(includePath);
            }

            return query;
        }

        public IQueryable<TEntity> GetAllWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = DbSet.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.UpdatedOn = DateTime.UtcNow;
            DbSet.Add(entity);

            SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);

            SaveChanges();
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdatedOn = DateTime.UtcNow;
            DbSet.Update(entity);

            SaveChanges();
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}