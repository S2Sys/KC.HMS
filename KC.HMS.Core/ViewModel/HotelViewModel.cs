namespace KC.HMS.Core.ViewModel
{
    public class HotelViewModel
    {
        public HotelModel Info { get; set; }
        public IList<HotelRoomsModel> Rooms { get; set; } = new List<HotelRoomsModel>();

        public IList<Feature> Features { get; set; } = new List<Feature>();
    }

}
