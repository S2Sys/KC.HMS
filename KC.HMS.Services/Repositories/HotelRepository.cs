using Dapper;
using KC.HMS.Core.Abstracts;
using KC.HMS.Services.Contracts;
using KC.HMS.Core.Domain;

using System.Data;
using KC.HMS.Core.ViewModel;
using KC.HMS.Core.Extentions;
using KC.HMS.Core;
using KC.HMS.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
 

namespace KC.HMS.Services.Repositories
{

    // services.AddScoped<IHotelRepository, HotelRepository>();
    public class HotelRepository : DapperDatabase, IHotelRepository
    {
        public HotelRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        #region Upsert or Cancel 
        public async Task<RoomViewModel> UpsertRoom(RoomViewModel item)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RoomID", item.RoomID, DbType.Int32);
                parameters.Add("@HotelID", item.HotelID, DbType.Int32);
                parameters.Add("@RoomNumber", item.RoomNumber, DbType.AnsiString);
                parameters.Add("@RoomTypeId", item.RoomTypeId, DbType.Int32);
                parameters.Add("@BathRooms", item.BathRooms, DbType.Int32);
                parameters.Add("@SquareFeet", item.SquareFeet, DbType.String);
                parameters.Add("@BasicCost", item.BasicCost, DbType.Decimal);
                parameters.Add("@AdditonalBeds", item.AdditonalBeds, DbType.Int32);
                parameters.Add("@AdditonalBedCost", item.AdditonalBedCost, DbType.Decimal);

                var taskResult = await Connection.QueryAsync<RoomViewModel>(
                                 sql: "UpsertRooms",
                                 param: parameters,
                                 commandType: CommandType.StoredProcedure);


                return taskResult.FirstOrDefault();
            }
            catch (Exception exception)
            {
                HandleError(exception);
                return null;
            }
        }

        public async Task<TVPBooking> UpsertBooking(TVPBooking item)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookingId", item.BookingId, DbType.Guid);
                parameters.Add("@RoomId", item.RoomId, DbType.Int32);
                parameters.Add("@CheckIn", item.CheckIn, DbType.DateTime);
                parameters.Add("@CheckOut", item.CheckOut, DbType.DateTime);
                // parameters.Add("@TotalCost", item.TotalFee, DbType.Decimal);
                parameters.Add("@Paid", item.Paid, DbType.Decimal);
                parameters.Add("@Completed", item.Completed, DbType.Boolean);
                parameters.Add("@ApplicationUserId", item.ApplicationUserId, DbType.AnsiString);
                parameters.Add("@CustomerName", item.CustomerName, DbType.AnsiString);
                parameters.Add("@CustomerEmail", item.CustomerEmail, DbType.AnsiString);
                parameters.Add("@CustomerPhone", item.CustomerPhone, DbType.AnsiString);
                parameters.Add("@CustomerAddress", item.CustomerAddress, DbType.AnsiString);
                parameters.Add("@CustomerCity", item.CustomerCity, DbType.AnsiString);
                parameters.Add("@OptForAdditionalBed", item.OptForAdditionalBed, DbType.Boolean);
                parameters.Add("@BookingStatus", item.BookingStatus, DbType.Int32);
                parameters.Add("@ReferenceId", item.ReferenceId, DbType.Guid);

                var taskResult = await Connection.ExecuteAsync(
                                    sql: "UpsertBooking",
                                    param: parameters,
                                    commandType: CommandType.StoredProcedure);

                return new TVPBooking();

            }
            catch (Exception exception)
            {
                HandleError(exception);
                return new TVPBooking();
            }
        }

        public async Task<List<TVPBooking>> UpsertBulkBooking(List<TVPBooking> bookingDtos)
        {

            var tvpBookingData = bookingDtos.ToDataTable();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@bookings", tvpBookingData
                    .AsTableValuedParameter("TVPBooking"));

                var taskResult = await Connection.QueryAsync<TVPBooking>(
                                    sql: "UpsertBookings",
                                    param: parameters,
                                    commandType: CommandType.StoredProcedure);

                return taskResult.ToList();

            }
            catch (Exception exception)
            {
                HandleError(exception);
                return null;
            }
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId, string userId)
        {
            var result = false;
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingId, DbType.Guid);
                parameters.Add("@ApplicationUserId", userId, DbType.AnsiString);

                var taskResult = await Connection.ExecuteAsync(
                    sql: "[dbo].[CancelBooking]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                result = taskResult > 0;

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }
        #endregion

        #region Read
        public async Task<HotelViewModel> GetHotelInfoAsync(int id = 2)
        {
            var result = new HotelViewModel();
            try
            {
                var parameters = new DynamicParameters();
                await QueryMultipleAsync(sql: "GetHotel",
               (reader) =>
               {
                   result.Info = reader.Read<HotelModel>().FirstOrDefault();
                   result.Rooms = reader.Read<HotelRoomsModel>().ToList();
                   result.Features = reader.Read<Feature>().ToList();
               },
               commandType: System.Data.CommandType.StoredProcedure
               );

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public async Task<IList<RoomViewModel>> GetAvailableRooms(DateTime checkin = default, DateTime checkout = default, int hotel = 2)
        {
            var result = new List<RoomViewModel>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@CheckIn", checkin, DbType.DateTime);
                parameters.Add("@CheckOut", checkout, DbType.DateTime);

                var taskResult = await Connection.QueryAsync<RoomViewModel>(
                    sql: "GetAvailableRooms",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                result = taskResult.ToList();

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public async Task<RoomViewModel> GetRoom(string name, int roomId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@name", name, DbType.AnsiString);
            parameters.Add("@id", roomId, DbType.Int32);
            var taskResult = await Connection.QueryAsync<RoomViewModel>(
                                   sql: "[dbo].[GetRoomByNameOrID]",
                                   param: parameters,
                                   commandType: CommandType.StoredProcedure);

            return taskResult.FirstOrDefault();
        }

        public async Task<List<RoomViewModel>> GetRooms()
        {
            var result = new List<RoomViewModel>();
            try
            {
                var parameters = new DynamicParameters();


                var taskResult = await Connection.QueryAsync<RoomViewModel>(
                    sql: "GetMasterRooms",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                result = taskResult.ToList();

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public async Task<List<BookRoomViewModel>> GetMyBooking(string userId, BookingStatusKind status)
        {
            var result = new List<BookRoomViewModel>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId, DbType.AnsiString);
                parameters.Add("@status", (int)status, DbType.Int64);

                var taskResult = await Connection.QueryAsync<BookRoomViewModel>(
                    sql: "[dbo].[GetBookingPaging]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                result = taskResult.ToList();

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        //public bool IsExists(int hotelId, int roomId, string name)
        //{
        //    var sp = "[dbo].[GetMasterRoomsExists]";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@hotelId", hotelId, DbType.Int32);
        //    parameters.Add("@roomId", roomId, DbType.Int32);
        //    parameters.Add("@Name", name, DbType.AnsiString);
        //    parameters.Add("@IsExists", name, DbType.Boolean, ParameterDirection.Output);
        //    var taskResult = Connection.ExecuteScalar<bool>(sp, parameters);

        //    return parameters.Get<bool>("@IsExists");

        //}

        public async Task<LookupViewModel> GetLookupAsync(LookupFormKind form, int id = 2)
        {
            var result = new LookupViewModel();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@hotelId", id, DbType.Int32);
                parameters.Add("@form", form.ToString(), DbType.AnsiString);

                await QueryMultipleAsync(sql: "[dbo].[GetLookup]",
                   (reader) =>
                   {
                       result.Types = reader.Read<SelectListItem>().ToList();
                       result.Hotels = reader.Read<SelectListItem>().ToList();
                   },
               commandType: System.Data.CommandType.StoredProcedure,
                parameters: parameters
               );

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        }

        public async Task<List<DashboardCalenderViewModel>> GetDashboardAsync()
        {
            var result = new List<DashboardCalenderViewModel>();
            try
            {
                var parameters = new DynamicParameters();


                await QueryMultipleAsync(sql: "GetDashboard",
                   (reader) =>
                   {
                       result = reader.Read<DashboardCalenderViewModel>().ToList();

                   },
               commandType: System.Data.CommandType.StoredProcedure,
                parameters: parameters
               );

            }
            catch (Exception exception)
            {
                HandleError(exception);
            }

            return result;
        } 
        #endregion
    }
}



