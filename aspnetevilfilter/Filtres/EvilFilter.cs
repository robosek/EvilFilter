using Microsoft.AspNetCore.Mvc.Filters;
using aspnetevilfilter.Models;

namespace aspnetevilfilter.Filtres
{
    public class EvilFilter : ActionFilterAttribute
    {
        private readonly EvilStatusCalculator _statusCalculator;
        
        public EvilFilter(EvilStatusCalculator statusCalculator)
        {
             _statusCalculator = statusCalculator;
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            actionContext.HttpContext.Response.Headers.Add("Goyello","Not funny easter egg");
            int currentStatusCode = actionContext.HttpContext.Response.StatusCode;
            actionContext.HttpContext.Response.StatusCode = _statusCalculator.CalculateEvilStatusCode(currentStatusCode);
        }
    }
}