using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KC.HMS.Core.Contracts
{
    public interface IValidateEntity<T> where T : class
    {
        ModelStateDictionary Validate(ModelStateDictionary modelState,T dto);
    }

}
