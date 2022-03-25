namespace KC.HMS.Services.Contracts
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IHotelRepository hotelRepository,
            IRoomRepository roomRepository)
        {
            Hotels = hotelRepository;
            Rooms = roomRepository;
        }
        public IHotelRepository Hotels { get; }
        public  IRoomRepository Rooms { get; }
    }
}
