using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC.HMS.Core.Abstracts
{
    public abstract class BaseDbAccess : IDisposable
    {
        #region Private Fields 

        protected IDbConnection _connection;
        protected IDbCommand _command;
        protected IDbTransaction _transaction;
        protected string _connectionString;
        protected string _provider;
        #endregion

        #region Constructors


        /// <summary>
        /// Constructor to create DbContext from connection string given "Name"
        /// </summary>
        public BaseDbAccess(string connetionString, string providerName)
        {
            if (string.IsNullOrWhiteSpace(connetionString))
            {
                throw new ArgumentException("Connection string cannot be empty", "connectionStringName");
            }
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentException("providerName cannot be empty", "providerName");
            }

            _connection = GetConnection(connetionString, providerName);
        }


        /// <summary>
        /// Constructor to create DbContext from connection string given "ConnectionStringSettings"
        /// </summary>
        public BaseDbAccess(ConnectionStringSettings connetionStringSetting)
        {
            if (string.IsNullOrWhiteSpace(connetionStringSetting.ConnectionString) || string.IsNullOrWhiteSpace(connetionStringSetting.ProviderName))
            {
                throw new ArgumentException("Connection string ConnectionString,ProviderName cannot be empty", connetionStringSetting.Name);
            }

            _connection = GetConnection(connetionStringSetting.ConnectionString, connetionStringSetting.ProviderName);
        }

        #endregion

        #region Public Properties


        /// <summary>
        /// Get Transaction (IDbTransaction)
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        /// <summary>
        /// Get Connection (IDbConnection)
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public IDbCommand Command
        {
            get
            {
                return _command;
            }
        }

        #endregion


        #region Private Methods 

        protected IDbConnection GetConnection(string connectionString, string provider)
        {
            IDbConnection connection = null;

            try
            {
                var factory = System.Data.Common.DbProviderFactories.GetFactory(provider);

                connection = factory.CreateConnection();

                connection.ConnectionString = connectionString;

            }
            catch
            {
                connection = null;
            }
            return connection;
        }

        protected IDbCommand GetCommand(string query, IEnumerable<KeyValuePair<string, IConvertible>> parameters, CommandType commandType)
        {
            IDbCommand _command = _connection.CreateCommand();

            _command.CommandType = commandType;
            _command.CommandText = query;

            Initialize();

            _command.Connection = _connection;

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    var parameter = _command.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value;
                    _command.Parameters.Add(parameter);
                }
            }

            return _command;
        }


        public void Initialize()
        {
            if (_connection == null)
            {
                return;
            }

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            //_transaction = _connection.BeginTransaction();
        }

        public void Clear()
        {
            try
            {

                if (_command != null)
                {
                    _command.Dispose();
                }

                _transaction.Dispose();
                _connection.Dispose();

            }
            finally
            {
                _connection = null;
                _transaction = null;
                if (_command != null)
                {
                    _command = null;
                }
            }
        }


        #endregion

        #region IDisposable Support
        protected bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
                if (_command != null)
                {
                    _command.Dispose();
                }
                _transaction?.Dispose();

                _connection = null;
                _transaction = null;
                _command = null;
            }
            _disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
