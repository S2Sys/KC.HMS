using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace KC.HMS.Core.Domain
{

    public partial class RoomDto : IValidateEntity<RoomDto>
    {
        public ModelStateDictionary Validate(ModelStateDictionary modelState,RoomDto dto)
        {

            if (dto.RoomID == 0)
                modelState.AddModelError("RoomID", "RoomID cannot be zero.");

            if (dto.HotelID == 0)
                modelState.AddModelError("HotelID", "HotelID cannot be zero.");
            return modelState;
        }
    }
}
