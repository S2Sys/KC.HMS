using Dapper;
using System.Data;

namespace KC.HMS.Core.Services
{
    public  class ValidateService : DapperDatabase, IValidateService
    {
        public ValidateService(IDbConnection dbConnection) : base(dbConnection)
        {

        }
        public bool IsRoomExists(int hotelId, int roomId, string name)
        {

            var sp = "[dbo].[GetMasterRoomsExists]";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@hotelId", hotelId, DbType.Int32);
            parameters.Add("@roomId", roomId, DbType.Int32);
            parameters.Add("@Name", name, DbType.AnsiString);
            parameters.Add("@IsExists", name, DbType.Boolean, ParameterDirection.Output);
            var taskResult = Connection.ExecuteScalar<bool>(sp, parameters);

            return parameters.Get<bool>("@IsExists");
        }
    }
}
