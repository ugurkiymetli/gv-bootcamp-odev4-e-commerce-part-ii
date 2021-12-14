using Emerce_API.Infrastructure;
using Emerce_Model;
using Emerce_Model.User;
using Emerce_Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
namespace Emerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController/*ControllerBase*/
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;
        public AuthController( IUserService _userService, IMemoryCache _memoryCache ) : base(_memoryCache)
        {
            userService = _userService;
            memoryCache = _memoryCache;
        }

        //Login User = takes General object and returns general object with isSuccess : true/false
        [HttpPost]
        [Route("login")]
        //first get from memory cache. than check if entity not empty and user matches with login user.
        //
        public General<UserViewModel> Login( [FromBody] UserLoginModel loginUser )
        {
            General<UserViewModel> response = new() { Entity = null };
            //gets loginUser from memoryCache directly.
            //General<UserViewModel> _response = new() { Entity = memoryCache.Get<UserViewModel>("Login") };

            //gets loginUser from Base.Controller.CurrentUser 
            General<UserViewModel> _response = new() { Entity = CurrentUser };
            if ( _response.Entity is not null && _response.Entity.Username == loginUser.Username )
            {
                response.Entity = _response.Entity;
                response.IsSuccess = true;
            }
            else
            {
                _response = userService.Login(loginUser);
                if ( _response.IsSuccess )
                {
                    var cacheOptiopns = new MemoryCacheEntryOptions() { AbsoluteExpiration = System.DateTime.Now.AddMinutes(10) };
                    memoryCache.Set("Login", _response.Entity, cacheOptiopns);
                    response.Entity = _response.Entity;
                    response.IsSuccess = true;
                }
                response.ExceptionMessage = _response.ExceptionMessage;
            }

            return response;
        }
        [HttpGet]
        [LoginFilter]
        [Route("logout")]
        public General<bool> Logout()
        {
            General<bool> response = new();
            if ( CurrentUser.Id <= 0 )
            {
                response.ExceptionMessage = "You are not logged in!";
                return response;
            }
            memoryCache.Remove("Login");
            response.IsSuccess = true;
            return response;
        }
    }
}
