using System.Configuration;
using System.Data;
using System.Data.Common;

namespace KC.HMS.Core.Abstracts
{
    //https://dapper-tutorial.net/knowledge-base/31298235/how-to-implement-unit-of-work-pattern-with-dapper-
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly string _provider;

        internal UnitOfWork(ConnectionStringSettings connection)
        {
            _provider = connection.ProviderName;
            _connectionString = connection.ConnectionString;

            _id = Guid.NewGuid();
            _connection = GetConnection();
            _connection.Open();
        }

        internal UnitOfWork(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
            _connection.Open();
        }

        #region Private Methods 
        public IDbConnection GetConnection()
        {
            IDbConnection _dbConnection = null;
            try
            {
                var factory = DbProviderFactories.GetFactory(_provider);

                _dbConnection = factory.CreateConnection();

                _dbConnection.ConnectionString = _connectionString;

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
            return _dbConnection;
        }


        private void HandleError(Exception exception)
        {
            throw exception;
        }

        #endregion

        private readonly IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        private Guid _id = Guid.Empty;

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = null;
        }

        //protected IDbConnection GetConnection(string connectionString, string provider)
        //{
        //    IDbConnection connection = null;

        //    try
        //    {
        //        var factory = DbProviderFactories.GetFactory(provider);

        //        connection = factory.CreateConnection();

        //        connection.ConnectionString = connectionString;

        //    }
        //    catch
        //    {
        //        connection = null;
        //    }
        //    return connection;
        //}

    }
}
