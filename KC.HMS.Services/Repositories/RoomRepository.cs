using KC.HMS.Core.Abstracts;
using KC.HMS.Services.Contracts;
using KC.HMS.Core.Domain;

using System.Data;

namespace KC.HMS.Services.Repositories
{
    // services.AddScoped<IRoomRepository, RoomRepository>();
    public class RoomRepository : DapperDatabase, IRoomRepository
    {
        public RoomRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public Task<int> AddAsync(RoomDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RoomDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(RoomDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
