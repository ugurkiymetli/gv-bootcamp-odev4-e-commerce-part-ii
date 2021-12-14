using Emerce_Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
namespace Emerce_API.Infrastructure
{
    public class BaseController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        public BaseController( IMemoryCache _memoryCache )
        {
            memoryCache = _memoryCache;
        }


        public UserViewModel CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }

        private UserViewModel GetCurrentUser()
        {
            var response = new UserViewModel();
            if ( memoryCache.TryGetValue("Login", out UserViewModel loginUser) )
            {
                response = loginUser;
            }

            return response;
        }
    }
}
