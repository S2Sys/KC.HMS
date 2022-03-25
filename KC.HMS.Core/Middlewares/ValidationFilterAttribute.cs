using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KC.HMS.Core.Middlewares
{
    /*
        IN STARTUP.CS ENBALE SERVICEFILTER GLOBALLY
        ******************************************* 
          services.AddScoped<ValidationFilterAttribute<Model>>();
          services.Configure<ApiBehaviorOptions>(options =>
          {
                options.SuppressModelStateInvalidFilter = true;
          });
        IN CONTROLLER
        ******************************************* 
            [ServiceFilter(typeof(ValidationFilterAttribute<EntityDTO>))] 
     IN MODEL
        ******************************************* 
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
    */
    public class ValidationFilterAttribute<T> : IActionFilter where T : class, IValidateEntity<T>
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

            DoBusinessValidation(param.Value, context.ModelState);

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static void DoBusinessValidation(object param, ModelStateDictionary modelState)
        {
            if (param == null)
                return;
            else
            {
                T entity = param as T;
                var type = typeof(T);

                var instance = Activator.CreateInstance(type) as IValidateEntity<T>;
                var doValidate = type.GetMethod("Validate");
                if (instance != null && doValidate != null)
                    modelState = instance.Validate(modelState, entity);
            }
        } 
       
    }
}

