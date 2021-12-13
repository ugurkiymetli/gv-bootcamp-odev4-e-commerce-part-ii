using Emerce_Model;
using Emerce_Model.User;
using Emerce_Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;
        public UserController( IUserService _userService, IMemoryCache _memoryCache )
        {
            userService = _userService;
            memoryCache = _memoryCache;
        }

        //Insert User returns General Object with IsSuccess, ErrorList, Posted Data...
        [HttpPost]
        [Route("register")]
        public General<UserCreateModel> Insert( [FromBody] UserCreateModel newUser )
        {
            return userService.Insert(newUser);
        }

        //Login User = takes General object and returns general object with isSuccess : true/false
        [HttpPost]
        [Route("login")]
        //first get from memory cache. than check if entity not empty and user matches with login user.
        //
        public General<bool> Login( [FromBody] UserLoginModel loginUser )
        {
            General<bool> response = new() { Entity = false };
            General<UserViewModel> _response = new() { Entity = memoryCache.Get<UserViewModel>("Login") };

            if ( _response.Entity is not null && _response.Entity.Username == loginUser.Username )
            {
                response.Entity = true;
                response.IsSuccess = true;
            }
            else
            {
                _response = userService.Login(loginUser);
                if ( _response.IsSuccess )
                {
                    var cacheOptiopns = new MemoryCacheEntryOptions() { AbsoluteExpiration = System.DateTime.Now.AddMinutes(10) };
                    memoryCache.Set("Login", _response.Entity, cacheOptiopns);
                    response.Entity = true;
                    response.IsSuccess = true;
                }
                response.ExceptionMessage = _response.ExceptionMessage;
            }

            return response;
        }


        //Get All User = returns users in General object List
        [HttpGet]
        public General<UserViewModel> Get()
        {
            return userService.Get();
        }
        //Update User
        [HttpPut("{id}")]
        public General<UserViewModel> Update( [FromBody] UserUpdateModel updatedUser, int id )
        {
            return userService.Update(updatedUser, id);
        }
        //Delete User = throws ex if user is not found
        [HttpDelete("{id}")]
        public General<UserViewModel> Delete( int id )
        {
            return userService.Delete(id);
        }

    }
}
