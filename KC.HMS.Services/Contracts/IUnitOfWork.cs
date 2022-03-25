namespace KC.HMS.Services.Contracts
{
    public interface IUnitOfWork
    {

        IHotelRepository Hotels { get; }
        IRoomRepository Rooms { get; }
    }
}
