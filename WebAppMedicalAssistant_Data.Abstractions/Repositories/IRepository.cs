using System.Linq.Expressions;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Data.Abstractions.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
    {
        //READ
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Get();
        IQueryable<T> FindBy(Expression<Func<T, bool>> searchExpression, params Expression<Func<T, object>>[] includes);

        //CREATE
        Task AddEntityAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        //UPDATE
        void Update(T entity);
        Task PatchAsync(int id, List<PatchModel> patchData);

        //DELETE
        void Remove(T entity);
    }
}
