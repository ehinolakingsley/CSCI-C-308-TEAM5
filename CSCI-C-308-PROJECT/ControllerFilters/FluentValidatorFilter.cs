using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CSCI_308_TEAM5.API.Actions.BaseAction;

namespace CSCI_308_TEAM5.API.ControllerFilters
{
    internal sealed class FluentValidatorFilter : IAsyncActionFilter
    {
        static string[] acceptedMethods => ["PUT", "POST", "PATCH"];

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is ControllerBase)
            {
                var x = context.HttpContext.Features.Get<IHttpRequestFeature>();

                if (!acceptedMethods.Contains(x.Method))
                {
                    await next();
                    return;
                }

                var serviceProvider = context.HttpContext.RequestServices;

                foreach (var parameter in context.ActionDescriptor.Parameters)
                {
                    if (context.ActionArguments.TryGetValue(parameter.Name, out var argument) && serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameter.ParameterType)) is IValidator validator)
                    {
                        var validate = await validator.ValidateAsync(new ValidationContext<object>(argument));

                        if (!validate.IsValid)
                        {
                            context.Result = new APIResponseHandler(System.Net.HttpStatusCode.BadRequest, validate.Errors[0].ErrorMessage);
                            return;
                        }
                    }
                }
            }

            await next();
        }
    }
}
