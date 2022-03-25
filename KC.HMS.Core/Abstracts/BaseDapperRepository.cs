using Dapper;
using KC.HMS.Core.Settings;
using System.Data;
using System.Data.Common;

namespace KC.HMS.Core.Abstracts
{
    public class BaseDapperRepository
    {
        #region Constractors 
        protected IDbConnection _dbConnection { get; set; }


        public BaseDapperRepository(IDbConnection DbConnection)
        {

            _dbConnection = DbConnection;
        }
        public BaseDapperRepository(string connectionString, string provider)
        {

            _dbConnection = GetConnection(connectionString, provider);
        }

        public BaseDapperRepository(ConnectionString connectionString)
        {

            _dbConnection = GetConnection(connectionString.Value, connectionString.Provider);
        }

        #endregion

        #region Private Methods 
        private IDbConnection GetConnection(string connectionString, string provider)
        {
            IDbConnection _dbConnection = null;
            try
            {
                var factory = DbProviderFactories.GetFactory(provider);

                _dbConnection = factory.CreateConnection();

                _dbConnection.ConnectionString = connectionString;

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

        #region DB Async Methods 

        public async Task<int> ExecuteAsync(string sql,
            DynamicParameters parameters = null,
            bool allowTransation = true,
            CommandType commandType = CommandType.StoredProcedure)
        {
            var affectedRows = -1;
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();

                    if (!allowTransation)
                    {
                        affectedRows = await _connection.ExecuteAsync(sql: sql, param: parameters, commandType: commandType);
                    }
                    else
                    {
                        using (var transaction = _connection.BeginTransaction())
                        {
                            try
                            {
                                affectedRows = await _connection.ExecuteAsync(sql, param: parameters, commandType: commandType, transaction: transaction);
                                transaction.Commit();
                            }
                            catch (Exception exception)
                            {
                                transaction.Rollback();
                                HandleError(exception);
                            }
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
            return affectedRows;

        }


        public async Task<IList<T>> QueryAsync<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(IEnumerable<T>);
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    result = await _dbConnection.QueryAsync<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result.ToList();
        }


        public async Task<T> QuerySingleAsync<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(T);
            try
            {
                using (var _connection = _dbConnection)
                {

                    _connection.Open();
                    result = await _connection.QueryFirstOrDefaultAsync<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public async Task QueryMultipleAsync(string sql,
           Action<SqlMapper.GridReader> callback,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {

            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    var gridReader = await _connection.QueryMultipleAsync(sql: sql, param: parameters, commandType: commandType);
                    callback(gridReader);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(T);
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    result = await _connection.ExecuteScalarAsync<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }


        #endregion

        #region DB Async Methods 

        public int Execute(string sql,
            DynamicParameters parameters = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            var affectedRows = -1;
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    affectedRows = _connection.Execute(sql: sql, param: parameters, commandType: commandType);

                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
            return affectedRows;
        }

        public IList<T> Query<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(IList<T>);
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    result = _connection.Query<T>(sql: sql, param: parameters, commandType: commandType).ToList();

                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public T QuerySingle<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(T);
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    result = _connection.QueryFirstOrDefault<T>(sql: sql, param: parameters, commandType: commandType);

                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public void QueryMultiple(string sql,
          Action<SqlMapper.GridReader> callback,
          DynamicParameters parameters = null,
          CommandType commandType = CommandType.StoredProcedure)
        {

            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    var gridReader = _connection.QueryMultiple(sql: sql, param: parameters, commandType: commandType);
                    callback(gridReader);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
        }

        public T ExecuteScalar<T>(string sql,
           DynamicParameters parameters = null,
           CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(T);
            try
            {
                using (var _connection = _dbConnection)
                {
                    _connection.Open();
                    result = _connection.ExecuteScalar<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }


        #endregion

        #region Upsert Auto/Merge

        public void UpsertOrMerge<TEntity>(TEntity entity, string schema = "dbo") where TEntity : class
        {
            using (var _connection = _dbConnection)
            {
                _connection.Open();
                var properties = entity.GetType().GetProperties();
                var keyColumn = properties[0];
                foreach (var property in properties)
                {
                    if (property.CustomAttributes != null)
                    {
                        bool hasKey = property.CustomAttributes.Any(i => i.AttributeType.Name == "KeyAttribute");
                        if (hasKey)
                        {
                            keyColumn = property;
                            break;
                        }
                    }
                }

                var props = properties.Select(p => p.Name).ToList();
                var key = keyColumn.Name;

                var names = string.Join(", ", props);
                var values = string.Join(", ", props.Select(n => "@" + n));
                var updates = string.Join(", ", props.Select(n => $"{n} = @{n}"));
                var where = $"{key} = @{key}";

                var query = $@"MERGE [{schema}].[{entity.GetType().Name.Replace("DTO", "")}] as target
                              USING (VALUES({values}))
                              AS SOURCE ({names})
                              ON target.{where}
                              WHEN matched THEN
                                UPDATE SET {updates}
                              WHEN not matched THEN
                                INSERT({names}) VALUES({values});";

                _connection.Execute(query, entity);
            }
        }

        #endregion


    }
}
