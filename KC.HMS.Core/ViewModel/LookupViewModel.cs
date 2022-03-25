using Microsoft.AspNetCore.Mvc.Rendering;

namespace KC.HMS.Core.ViewModel
{
    public class LookupViewModel
    {
        public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Hotels { get; set; } = new List<SelectListItem>();
    }
}
