using Dapper;
using KC.HMS.Core.Extensions;
using KC.HMS.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace KC.HMS.Core.Validation
{
    public class RoomExistsAttribute : ValidationAttribute
    {

        private readonly string _Message = "Room Number is already exists.";
        private readonly IDbConnection _dbConnection;
        public RoomExistsAttribute()
        {
            //_dbConnection = dbConnection;
        }

        public RoomExistsAttribute(string message)
        {
            _Message = message;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IValidateService)validationContext.GetService(typeof(IValidateService));

            var model = (RoomViewModel)validationContext.ObjectInstance;

            var dbConnection = (IDbConnection)validationContext.GetService(typeof(IDbConnection));

            var isExists = true;

            var sp = "[dbo].[GetMasterRoomsExists]";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@hotelId", model.HotelID, DbType.Int32);
            parameters.Add("@roomId", model.RoomID, DbType.Int32);
            parameters.Add("@Name", model.RoomNumber, DbType.AnsiString);
            parameters.Add("@ActionStatus", isExists, DbType.Boolean, ParameterDirection.Output);
            var taskResult = DapperDb.Query<bool>(sp, parameters: parameters);

            isExists = parameters.Get<bool>("@ActionStatus");

            // var isExists = service.IsRoomExists(model.HotelID, model.RoomID, model.RoomNumber);
            if (isExists)
            {
                return new ValidationResult(_Message);
            }

            return ValidationResult.Success;
        }
    }
}
