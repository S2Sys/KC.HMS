namespace KC.HMS.Core.Contracts
{
    public interface IValidateService
    {
        bool IsRoomExists(int hotelId, int roomId, string name);
    }
}
