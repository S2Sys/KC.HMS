using KC.HMS.Core.Abstracts;

namespace KC.HMS.Web.Infrastructure.Abstracts
{
    public interface IGenricRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}
