using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Data.Abstractions.Repositories;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly MedicalAssistantContext DataBase;
        protected readonly DbSet<T> DbSet;

        public Repository(MedicalAssistantContext dataBase)
        {
            DataBase = dataBase;
            DbSet = dataBase.Set<T>();
        }

        //CREATE
        public virtual async Task AddEntityAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        //READ
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> searchExpression, params Expression<Func<T, object>>[] includes)
        {
            var result = DbSet.Where(searchExpression);
            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result;
        }

        public virtual IQueryable<T> Get()
        {
            return DbSet;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        //UPDATE
        public virtual async Task PatchAsync(int id, List<PatchModel> patchData)
        {
            var model = await DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

            var nameValuePropertiesPairs = patchData.ToDictionary(patchModel => patchModel.PropertyName, patchModel => patchModel.PropertyValue);

            var dbEntityEntry = DataBase.Entry(model);
            dbEntityEntry.CurrentValues.SetValues(nameValuePropertiesPairs);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        //DELETE
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
