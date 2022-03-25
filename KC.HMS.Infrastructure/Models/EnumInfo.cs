using System;

namespace KC.HMS.Infrastructure.Models
{
    public class EnumInfo
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Enum Value { get; set; }
    }
}
