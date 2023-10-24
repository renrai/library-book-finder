using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ProjectChallengeDomain.Models.Requests;
using FluentValidation;

namespace ProjectChallengeAPI.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(errors);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
