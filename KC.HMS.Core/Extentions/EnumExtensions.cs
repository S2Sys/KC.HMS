using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace KC.HMS.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum source)
        {
            try
            {

                DescriptionAttribute? attribute = source.GetType()
                    .GetField(source.ToString())
                    .GetCustomAttribute<DescriptionAttribute>(false);

                return attribute != null ? attribute.Description : source.ToString();
            }
            catch // Log nothing, just return an empty string
            {
                return string.Empty;
            }
        }


        public static List<SelectListItem> ToList(Type enumType)
        {
            if (enumType.IsEnum)
            {
                return Enum.GetValues(enumType)
                    .Cast<Enum>()

                    .Select(e => new SelectListItem()
                    {
                        Value = e.ToString(),
                        Text = e.Description()
                    })
                    .ToList();
            }

            return null;
        }
 
    }
}
