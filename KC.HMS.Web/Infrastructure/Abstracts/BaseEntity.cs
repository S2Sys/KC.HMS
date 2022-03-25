namespace KC.HMS.Web.Infrastructure.Abstracts
{
    public class BaseEntity
    {
        public Int64 Id
        {
            get;
            set;
        }
        public DateTime Created
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime Modified
        {
            get;
            set;
        }
        public string ModifiedBy
        {
            get;
            set;
        }
        public string IPAddress
        {
            get;
            set;
        }
    }

}
