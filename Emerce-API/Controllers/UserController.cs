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
    [LoginFilter]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        //private readonly IMemoryCache memoryCache;
        public UserController( IUserService _userService, IMemoryCache _memoryCache ) : base(_memoryCache)
        {
            userService = _userService;
            //memoryCache = _memoryCache;
        }

        //Insert User returns General Object with IsSuccess, ErrorList, Posted Data...
        [HttpPost]
        [Route("register")]
        public General<UserViewModel> Insert( [FromBody] UserCreateModel newUser )
        {
            newUser.Iuser = CurrentUser.Id;
            return userService.Insert(newUser);
        }

        //Get All User = returns users in General object List
        [HttpGet]
        public General<UserViewModel> Get()
        {
            return userService.Get();
        }

        //Get User By Id
        [HttpGet("{id}")]
        public General<UserViewModel> GetById( int id )
        {
            return userService.GetById(id);
        }

        //Update User
        [HttpPut("{id}")]
        public General<UserViewModel> Update( [FromBody] UserUpdateModel updatedUser, int id )
        {
            updatedUser.Uuser = CurrentUser.Id;
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
