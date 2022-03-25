using System.Data;

namespace KC.HMS.Core.Abstracts
{
    public abstract class DatabaseRepository
    {
        protected readonly IDbConnection _dbConnection;

        protected DatabaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }

}
