using Core.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityFramework
{///
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
where TEntity : BaseEntity, new()
where TContext : DbContext, new()
    {
        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            using (var context = new TContext())
            {
                //rollback entity changes
                if (context is DbContext dbContext)
                {
                    var entries = dbContext.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                    entries.ForEach(entry =>
                    {
                        try
                        {
                            entry.State = EntityState.Unchanged;
                        }
                        catch (InvalidOperationException)
                        {
                            // ignored
                        }
                    });
                }

                try
                {
                    context.SaveChanges();
                    return exception.ToString();
                }
                catch (Exception ex)
                {
                    //if after the rollback of changes the context is still not saving,
                    //return the full text of the exception that occurred when saving
                    return ex.ToString();
                }
            }
        }
        #endregion
        #region Methods
        #region Sync
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                try
                {
                    entity.Id = 0;
                    entity.IsActive = true;
                    entity.CreatedDate = DateTime.UtcNow;
                    entity.UpdatedDate = null;
                    context.Set<TEntity>().Add(entity);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
                }
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                try
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedDate = DateTime.Now;
                    context.Set<TEntity>().Update(entity);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
                }
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                try
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.IsActive = false;
                    entity.UpdatedDate = DateTime.Now;
                    context.Set<TEntity>().Update(entity);
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
                }
            }
        }
        public TEntity GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().AsNoTracking()
                        .FirstOrDefault(e => e.Id == id);
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }
        #endregion
        #region Async
        public async Task AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                try
                {
                    entity.Id = 0;
                    entity.IsActive = true;
                    entity.CreatedDate = DateTime.Now;
                    entity.UpdatedDate = null;
                    await context.Set<TEntity>().AddAsync(entity);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
                }
            }
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking()
                     .FirstOrDefaultAsync(e => e.Id == id);
            }
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
            }
        }
        #endregion
        #region Bulks
        #region Sync
        public void AddAll(List<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.AddRange(entity);
                context.SaveChanges();
            }
        }
        public void UpdateAll(List<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entity);
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        #endregion
        #region Async
        public async Task AddAllAsync(List<TEntity> entity)
        {
            using (var context = new TContext())
            {
                await context.AddRangeAsync(entity);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateAllAsync(List<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entity);
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return await (filter == null
                    ? context.Set<TEntity>().ToListAsync()
                    : context.Set<TEntity>().Where(filter).ToListAsync());
            }
        }
        #endregion
        #endregion
        #region Pagings
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            using (var context = new TContext())
            {
                int skip = page == 0 ? 0 : (page - 1) * pageSize;

                if (condition == null)
                    return await context.Set<TEntity>().Skip(skip).Take(pageSize).ToListAsync();
                else
                    return await context.Set<TEntity>().Where(condition).Skip(skip).Take(pageSize).ToListAsync();
            }
        }

        public int Count(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            using (var context = new TContext())
            {
                if (condition == null)
                    return context.Set<TEntity>().Count();
                else
                    return context.Set<TEntity>().Where(condition).Count();
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            using (var context = new TContext())
            {
                if (condition == null)
                    return await context.Set<TEntity>().CountAsync();
                else
                    return await context.Set<TEntity>().Where(condition).CountAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TEntity>> selectExpression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (include != null) query = include(query);
                if (predicate != null) query = query.Where(predicate);
                if (orderBy != null) query = orderBy(query);
                if (selectExpression != null) query = query.Select(selectExpression);
                return await query.ToListAsync();
            }
        }
        #endregion
        #region DapperQueries
        public async Task<T> GetSPAsync<T>(string SpName, DynamicParameters dynamicParameters = null)
        {
            using (var context = new TContext())
            {
                var connectionString = context.Database.GetDbConnection().ConnectionString;

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    await sqlcon.OpenAsync();
                    var response = await sqlcon.QueryAsync<T>(SpName, dynamicParameters, commandType: CommandType.StoredProcedure);
                    return (T)Convert.ChangeType(response.FirstOrDefault(), typeof(T));
                }
            }
        }

        public async Task<List<T>> GetAllSPAsync<T>(string SpName, DynamicParameters dynamicParameters = null)
        {
            using (var context = new TContext())
            {
                var connectionString = context.Database.GetDbConnection().ConnectionString;

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    await sqlcon.OpenAsync();
                    var response = await sqlcon.QueryAsync<T>(SpName, dynamicParameters, commandType: CommandType.StoredProcedure);
                    return (List<T>)response.ToList();
                }
            }
        }
        #endregion
        #endregion
        #region Properties       
        #endregion
    }
}
