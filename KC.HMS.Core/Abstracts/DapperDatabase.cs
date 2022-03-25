using Dapper;
using System.Data;

namespace KC.HMS.Core.Abstracts
{
    public abstract class DapperDatabase
    {
        protected readonly IDbConnection Connection;

        protected DapperDatabase(IDbConnection dbConnection)
        {
            this.Connection = dbConnection;
        }

        public async Task QueryMultipleAsync(string sql,
          Action<SqlMapper.GridReader> callback,
          DynamicParameters parameters = null,
          CommandType commandType = CommandType.StoredProcedure)
        {

            try
            {
                using (var _connection = this.Connection)
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

        public void HandleError(Exception exception)
        {
            throw exception;
        }

    }
}
