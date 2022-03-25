namespace KC.HMS.Web.Infrastructure.Abstracts
{
    public abstract class ValidationEntity<T>
    {
        public abstract ValidationResult Validate(T source);

        //{
        //    return new ValidationResult();
        //}
    }


}
