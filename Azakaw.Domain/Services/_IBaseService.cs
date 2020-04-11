using System.Collections.Generic;
using Azakaw.Domain.Models;

namespace Azakaw.Domain.Services
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        ResponseModel<TEntity> Create(TEntity entity);
        ResponseModel<TEntity> Update(TEntity entity);
        ResponseModel<bool> SoftDelete(TEntity entity);
        ResponseModel<bool> SoftDelete(int id);
        ResponseModel<TEntity> GetById(int id);
        ResponseModel<IEnumerable<TEntity>> GetAll();
        ResponseModel<IEnumerable<TEntity>> GetAll(int offset, int fetch);
    }
}