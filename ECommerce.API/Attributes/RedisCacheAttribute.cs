using ECommerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSeconds;
        public RedisCacheAttribute(int durationInSeconds = 60)
        {
          _durationInSeconds = durationInSeconds;  
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var data = await cacheService.GetDataAsync(cacheKey);
            if(!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
                {
                  Content = data,
                  ContentType = "application/json",
                  StatusCode= StatusCodes.Status200OK
                };
                return;
            }
            var executedContext = await next.Invoke();
            if (executedContext .Result is OkObjectResult { Value:not null}ok) 
            {
            await cacheService.SetDataAsync(cacheKey,ok.Value,TimeSpan.FromSeconds(90));
            
            }
        }


        private static string CreateCacheKey(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            if (request.Query.Any())
            {
                Key.Append("?");
                foreach (var (k, v) in request.Query.OrderBy(X => X.Key))
                {
                 Key.Append(k).Append("=").Append(v).Append('&');
                }
            }
            return Key.ToString();
        }
    }
}