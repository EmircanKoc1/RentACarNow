using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RentACarNow.WebAPI.Infrastructure
{
    public class ValidationActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //if (!context.ModelState.IsValid)
            //{
            //    var messages = context.ModelState.Values.SelectMany(x => x.Errors)
            //                                        .Select(x => !string.IsNullOrEmpty(x.ErrorMessage) ?
            //                                                x.ErrorMessage : x.Exception?.Message)
            //                                        .Distinct().ToList();

            //    var result = new ValidationResponseModel(messages);
            //    context.Result = new BadRequestObjectResult(result);

            //}

            return Task.CompletedTask;

        }
    }
}
