using Core.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type, must to from BaseEntity inheritance.</typeparam>
    public partial interface IEntityRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region Methods

        #region Sync
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int id);
        #endregion
        #region Async
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null);
        #endregion
        #region Bulks
        #region Sync
        void AddAll(List<TEntity> entity);
        void UpdateAll(List<TEntity> entity);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
       
        #endregion
        #region Async
        Task AddAllAsync(List<TEntity> entity);
        Task UpdateAllAsync(List<TEntity> entity);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        

        #endregion
        #endregion
        #region Pagings
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);
        int Count(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           Expression<Func<TEntity, TEntity>> selectExpression = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        #endregion
        #region DapperQueries

        Task<T> GetSPAsync<T>(string SpName, DynamicParameters dynamicParameters = null);
        Task<List<T>> GetAllSPAsync<T>(string SpName, DynamicParameters dynamicParameters = null);
        #endregion
        #endregion
        #region Properties
  
        #endregion
    }
}