using Boilerplate.Data.Context;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Interfaces;

namespace Boilerplate.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<bool> Insert(TEntity obj)
        {
            try
            {
                _databaseContext.Set<TEntity>().Add(obj);
                _databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public virtual async Task<IList<TEntity>> Select()
        {
            var entities = _databaseContext.Set<TEntity>().ToList();
            return entities;
        }

        public virtual async Task<TEntity?> Select(int id)
            => _databaseContext.Set<TEntity>().SingleOrDefault(x => x.Id == id);

        public virtual async Task<bool> Update(TEntity obj)
        {
            try
            {
                _databaseContext.Entry(obj).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                _databaseContext.Set<TEntity>().Remove(await Select(id));
                _databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
