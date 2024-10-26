using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Core.Services.Contract;
using System.Text;

namespace Project.APIs.Attributes
{
    public class CashedAttribute : Attribute , IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CashedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();
            var cacheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);
            var casheResponse = await casheService.GetCasheKeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(casheResponse)) 
            {
                var contentResult = new ContentResult()
                {
                    Content = casheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();
            if (executedContext.Result is OkObjectResult response) 
            {
                await casheService.SetCasheKeyAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
            }
        }

        private string GenerateCasheKeyFromRequest(HttpRequest request) 
        {
            var casheKey = new StringBuilder();
            casheKey.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key)) 
            {
                casheKey.Append($"|{key}-{value}");
            }
            return casheKey.ToString();
        }
    }
}
