using System;
using System.Collections.Generic;
using System.Linq;
using Azakaw.Domain;
using Azakaw.Domain.Models;
using Azakaw.Domain.Repositories;
using Azakaw.Domain.Services;

namespace Azakaw.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public virtual ResponseModel<TEntity> Create(TEntity entity)
        {
            BaseRepository.Insert(entity);

            var isCreated = entity.Id > 0;
            return isCreated ?
                ResponseModel<TEntity>.GetSuccessResponse(entity) :
                ResponseModel<TEntity>.GetFailureResponse(entity);
        }

        public virtual ResponseModel<IEnumerable<TEntity>> GetAll()
        {
            var entities = BaseRepository.GetAll();

            return entities.Any() ?
                ResponseModel<IEnumerable<TEntity>>.GetSuccessResponse(entities) :
                ResponseModel<IEnumerable<TEntity>>.GetNotFoundResponse();
        }

        public virtual ResponseModel<IEnumerable<TEntity>> GetAll(int offset, int fetch)
        {
            var entities = BaseRepository.GetAll().Skip(offset).Take(fetch);

            return entities.Any() ?
                ResponseModel<IEnumerable<TEntity>>.GetSuccessResponse(entities) :
                ResponseModel<IEnumerable<TEntity>>.GetNotFoundResponse();
        }

        public virtual ResponseModel<TEntity> GetById(int id)
        {
            var entity = BaseRepository.GetById(id);

            return entity?.Id > 0 ?
                ResponseModel<TEntity>.GetSuccessResponse(entity) :
                ResponseModel<TEntity>.GetNotFoundResponse();
        }

        public ResponseModel<bool> SoftDelete(int id)
        {
            var entity = BaseRepository.GetById(id);

            return SoftDelete(entity);
        }

        public virtual ResponseModel<bool> SoftDelete(TEntity entity)
        {
            if (entity == null) return ResponseModel<bool>.GetNotFoundResponse();

            entity.IsDeleted = true;
            var isUpdated = Update(entity).Status;

            return isUpdated ?
                ResponseModel<bool>.GetSuccessResponse(isUpdated) :
                ResponseModel<bool>.GetFailureResponse(isUpdated);
        }

        public virtual ResponseModel<TEntity> Update(TEntity entity)
        {
            try
            {
                BaseRepository.Update(entity);
                return ResponseModel<TEntity>.GetSuccessResponse(entity);
            }
            catch (Exception)
            {
                // todo: log exception

                return ResponseModel<TEntity>.GetFailureResponse(entity);
            }
        }
    }
}