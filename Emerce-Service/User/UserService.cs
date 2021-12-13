using AutoMapper;
using Emerce_DB;
using Emerce_Model;
using Emerce_Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Emerce_Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        public UserService( IMapper _mapper )
        {
            mapper = _mapper;
        }

        public General<UserLoginModel> Login( UserLoginModel user )
        {
            var result = new General<UserLoginModel>();
            var model = mapper.Map<Emerce_DB.Entities.User>(user);
            using ( var service = new EmerceContext() )
            {
                bool login = service.User
                   .Any(u => !u.IsDeleted && u.IsActive && u.Username == user.Username && u.Password == user.Password);
                model.Id = service.User.Where(u => u.Username == user.Username && u.Password == user.Password).Select(u => u.Id).FirstOrDefault();
                result.IsSuccess = login;
                result.Entity = mapper.Map<UserLoginModel>(model);
            }
            result.Entity.Password = "";
            return result;
        }

        public General<UserCreateModel> Insert( UserCreateModel newUser )
        {
            var result = new General<UserCreateModel>();
            var model = mapper.Map<Emerce_DB.Entities.User>(newUser);
            using ( var service = new EmerceContext() )
            {
                model.Idatetime = DateTime.Now;
                service.User.Add(model);
                service.SaveChanges();
                result.Entity = mapper.Map<UserCreateModel>(model);
                result.IsSuccess = true;
            }
            return result;
        }

        public General<UserViewModel> Get()
        {
            var result = new General<UserViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.User
                    .Where(u => u.IsActive && !u.IsDeleted)
                    .OrderBy(u => u.Id);
                result.List = mapper.Map<List<UserViewModel>>(data);
                result.IsSuccess = true;
                result.TotalCount = data.Count();
            }
            return result;
        }

        public General<UserUpdateModel> Update( UserUpdateModel updatedUser, int id )
        {
            var result = new General<UserUpdateModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.User.SingleOrDefault(u => u.Id == id);
                if ( data is null )
                {
                    result.ExceptionMessage = $"User with id: {id} is not found";
                    return result;
                }
                data.Username = String.IsNullOrEmpty(updatedUser.Username.Trim()) ? data.Username : updatedUser.Username;
                data.Email = String.IsNullOrEmpty(updatedUser.Email.Trim()) ? data.Email : updatedUser.Email;
                data.Password = String.IsNullOrEmpty(updatedUser.Password.Trim()) ? data.Password : updatedUser.Password;
                updatedUser.Udatetime = DateTime.Now;

                service.SaveChanges();
                result.Entity = mapper.Map<UserUpdateModel>(updatedUser);
                result.IsSuccess = true;
            }
            return result;
        }

        //void user delete
        /*public void Delete( int id )
        {
            using ( var service = new EmerceContext() )
            {
                var data = service.User.SingleOrDefault(u => u.Id == id);
                if ( data is null )
                    //throw new InvalidOperationException("This user is not found!!");
                    return;
                service.User.Remove(data);
                service.SaveChanges();
            }
        }*/
        //user delete
        public General<UserViewModel> Delete( int id )
        {
            var result = new General<UserViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.User.SingleOrDefault(u => u.Id == id);
                if ( data is null )
                {
                    result.ExceptionMessage = $"User with id: {id} is not found";
                    return result;
                }

                service.User.Remove(data);
                service.SaveChanges();
                result.Entity = mapper.Map<UserViewModel>(data);
                result.IsSuccess = true;
            }
            return result;
        }
    }
}
