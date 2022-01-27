using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.EntityFrameworkCore;

namespace SchoolManagement.Core.Repositories
{
    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly AppDbContext _dbContext;

        /// <summary>
        /// 通过泛型，从数据库上下文中获取领域模型
        /// </summary>
        public virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }
        public List<TEntity> GetAllList(Expression<Func<TEntity,bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public TEntity Single(Expression<Func<TEntity,bool>> predicate)
        {
            return GetAll().Single(predicate);
        }
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity,bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate)
        {
            var entity = await GetAll().FirstOrDefaultAsync(predicate);
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            var newEntity = Table.Add(entity).Entity;
            Save();
            return newEntity;
        }
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await Table.AddAsync(entity);
            await SaveAsync();
            return entityEntry.Entity;
        }
        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            Save();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            await SaveAsync();
        }
        public void Delete(Expression<Func<TEntity,bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }
        public async Task DeleteAsync(Expression<Func<TEntity,bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                await DeleteAsync(entity);
            }
        }

        /// <summary>
        /// 检查实体是否处于跟踪状态，如果是，则返回；如果不是，则添加跟踪状态
        /// </summary>
        /// <param name="entity"> </param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }
            Table.Attach(entity);
        }
        protected void Save()
        {
            //调用数据库上下文保存数据
            _dbContext.SaveChanges();
        }
        protected async Task SaveAsync()
        {
            //调用数据库上下文保存数据的异步方法
            await _dbContext.SaveChangesAsync();
        }

        public int Count()
        {
            return GetAll().Count();
        }
        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }
        public int Count(Expression<Func<TEntity,bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }
        public long LongCount()
        {
            return GetAll().LongCount();
        }
        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }
        public long LongCount(Expression<Func<TEntity,bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }
        public async Task<long> LongCountAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }
        public IQueryable<TEntity> FromSqlRaw(string sql)
        {
            return Table.FromSqlRaw(sql);
        }
        public IQueryable<TEntity> FromSqlInterpolated(FormattableString formattableString)
        {
            return Table.FromSqlInterpolated(formattableString);
        }
        public async Task<int> ExecuteSqlRawAsync(string sql)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql);
        }
        public int ExecuteSqlRaw(string sql)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql);
        }
        public int ExecuteSqlInterpolated(FormattableString formattableString)
        {
            return _dbContext.Database.ExecuteSqlInterpolated(formattableString);
        }
        public async Task<int> ExecuteSqlInterpolatedAsync(FormattableString formattableString)
        {
            return await _dbContext.Database.ExecuteSqlInterpolatedAsync(formattableString);
        }

        public async Task<List<IDictionary<string, object>>> ExecuteReaderAsync(string sql)
        {
            //https://stackoverflow.com/questions/17920146/find-the-datatype-of-field-from-datareader-object
            var metaDataList = new List<IDictionary<String, Object>>();
            //获取数据库的上下文连接
            var conn = _dbContext.Database.GetDbConnection();
            try
            { //打开数据库连接
                await conn.OpenAsync();
                //建立连接，因为非委托资源，所以需要使用using进行内存资源的释放
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;//赋值需要执行的SQL语句
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        //执行命令
                        if (reader.HasRows) //判断是否有返回行
                        {
                            while (await reader.ReadAsync())
                            {
                                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dictionary.Add(reader.GetName(i),reader[i]);
                                }
                                metaDataList.Add(dictionary);
                            }
                        }
                    }
                }
            }
            finally
            { //关闭数据库连接
                conn.Close();
            }

            return metaDataList;
        }
    }
}
