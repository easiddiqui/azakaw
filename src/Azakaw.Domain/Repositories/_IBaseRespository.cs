using System;
using System.Linq;
using System.Linq.Expressions;
using Azakaw.Domain.Models;

namespace Azakaw.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAllWithInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] includePaths);
        IQueryable<TEntity> GetAllWithInclude(params string[] includes);
        IQueryable<TEntity> GetAllWithInclude<TProperty>(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, TProperty>>[] includePaths);
        IQueryable<TEntity> GetAllWithInclude(Expression<Func<TEntity, bool>> predicate,
            params string[] includes);
    }
}