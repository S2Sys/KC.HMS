using System.Data;

namespace KC.HMS.Infrastructure.Abstracts
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
