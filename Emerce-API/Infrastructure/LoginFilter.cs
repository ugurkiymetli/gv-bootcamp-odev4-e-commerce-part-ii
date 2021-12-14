using Emerce_Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Emerce_API.Infrastructure
{
    public class LoginFilter : Attribute, IActionFilter
    {
        //private readonly IMemoryCache memoryCache;

        //public LoginFilter( IMemoryCache _memoryCache )
        //{
        //    memoryCache = _memoryCache;
        //}
        public void OnActionExecuted( ActionExecutedContext context )
        {
            return;
        }

        public void OnActionExecuting( ActionExecutingContext context )
        {
            var memoryCache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            if ( !memoryCache.TryGetValue("Login", out UserViewModel loginUser) )
            {
                context.Result = new BadRequestObjectResult("Please login!");
            }
            return;
        }
    }
}


