using KC.HMS.Core.Domain;

namespace KC.HMS.Core.ViewModel
{
    public class HotelModel : HotelDto
    {
        public Decimal MinCost { get; set; }
        public Decimal MaxCost { get; set; }
        public int MinSqFt { get; set; }
        public int MaxSqFt { get; set; }
    }

}
