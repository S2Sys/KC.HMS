using Microsoft.AspNetCore.Mvc.Filters;

namespace KC.HMS.Web.Infrastructure.Middlewares
{
    /*
        in startup.cs 
            services.AddScoped<ValidationFilterAttribute<Model>>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        In Controller
        [ServiceFilter(typeof(ValidationFilterAttribute<EntityDTO>))] 
    */
    public class ValidationFilterAttribute<T> : IActionFilter where T : class, IEntity
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is T);

            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Param is null/empty");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }
            var validationResult = BusinessValidation(param.Value);
            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(validationResult.Message);
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        //Need to upgarde this 
        private static ValidationResult BusinessValidation(object param)
        {
            object entity = param as T;
            var validationResult = new ValidationResult();

            var type = typeof(T);
            object classInstance = Activator.CreateInstance(type, null);
            var myMethod = type.GetMethod("Validate");
            if (myMethod != null)
            {
                var parameters = myMethod.GetParameters();
                object result = null;
                if (parameters.Length == 0)
                {
                    result = myMethod.Invoke(classInstance, null);
                }
                else
                {
                    object[] parametersArray = new object[] { entity };
                    result = myMethod.Invoke(classInstance, parametersArray);
                }
                validationResult = (ValidationResult)result;
            }
            return validationResult;
        }
    }
}

