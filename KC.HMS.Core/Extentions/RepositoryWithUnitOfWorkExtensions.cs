using KC.HMS.Core.Abstracts;

namespace KC.HMS.Core.Extensions
{
    public static class UOWRepositoryExtensions
    {
        #region Unit Of Work Create, Update & Delete

        /// <summary>
        /// Insert Item using Dapper & IUnitOfWork
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="repository"></param>
        /// <param name="entity">Entity To Insert</param>
        /// <param name="uow">IUnitOfWork</param>
        /// <returns>Inserted Entity with Updated Primary Key Value</returns>
        public static T Insert<T>(this IRepository<T> repository, T entity, IUnitOfWork uow) where T : class
        {
            uow?.Insert(entity);

            return entity;
        }

        /// <summary>
        /// Update item using Dapper & IUnitOfWork
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="repository"></param>
        /// <param name="entity">Entity To Update</param>
        /// <param name="uow">IUnitOfWork</param>
        /// <returns>Updated Entity</returns>
        public static T Update<T>(this IRepository<T> repository, T entity, IUnitOfWork uow) where T : class
        {
            uow?.Update(entity);

            return entity;
        }

        /// <summary>
        /// Delete item using Dapper & IUnitOfWork
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="repository"></param>
        /// <param name="entity">Entity To Delete</param>
        /// <param name="uow">IUnitOfWork</param>
        /// <returns>bool</returns>
        public static bool Delete<T>(this IRepository<T> repository, T entity, IUnitOfWork uow) where T : class
        {
            var isDeleted = false;

            uow?.Delete(entity);

            return isDeleted;
        }
        #endregion

    }
}
