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
        //Login User
        public General<UserViewModel> Login( UserLoginModel loginUser )
        {
            var result = new General<UserViewModel>();
            //var model = mapper.Map<Emerce_DB.Entities.User>(user);
            using ( var service = new EmerceContext() )
            {
                var data = service.User.FirstOrDefault(u => !u.IsDeleted && u.IsActive && u.Username == loginUser.Username && u.Password == loginUser.Password);
                if ( data is null )
                {
                    result.ExceptionMessage = $"User '{loginUser.Username}' not found or password is wrong.";
                    return result;
                }
                if ( data is not null )
                {
                    result.IsSuccess = true;
                    result.Entity = mapper.Map<UserViewModel>(data);
                }
            }
            return result;
        }
        //Insert User
        public General<UserViewModel> Insert( UserCreateModel newUser )
        {
            var result = new General<UserViewModel>();
            var model = mapper.Map<Emerce_DB.Entities.User>(newUser);
            using ( var service = new EmerceContext() )
            {
                model.Idatetime = DateTime.Now;
                model.IsActive = true;
                service.User.Add(model);
                service.SaveChanges();
                result.Entity = mapper.Map<UserViewModel>(model);
                result.IsSuccess = true;
            }
            return result;
        }

        //Get User List
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


        //Get User By Id
        public General<UserViewModel> GetById( int id )
        {
            var result = new General<UserViewModel>();

            using ( var service = new EmerceContext() )
            {
                var data = service.User.SingleOrDefault(u => u.Id == id && u.IsActive && !u.IsDeleted);
                if ( data is null )
                {
                    result.ExceptionMessage = $"User with id:{id} is not found";
                    return result;
                }
                result.IsSuccess = true;
                result.Entity = mapper.Map<UserViewModel>(data);
            }
            return result;
        }

        //Update User
        public General<UserViewModel> Update( UserUpdateModel updatedUser, int id )
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
                data.Udatetime = DateTime.Now;
                data.Username = String.IsNullOrEmpty(updatedUser.Username.Trim()) ? data.Username : updatedUser.Username;
                data.Email = String.IsNullOrEmpty(updatedUser.Email.Trim()) ? data.Email : updatedUser.Email;
                data.Password = String.IsNullOrEmpty(updatedUser.Password.Trim()) ? data.Password : updatedUser.Password;

                service.SaveChanges();
                result.Entity = mapper.Map<UserViewModel>(updatedUser);
                result.IsSuccess = true;
            }
            return result;
        }

        //Delete User
        public General<UserViewModel> Delete( int id )
        {
            var result = new General<UserViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.User.SingleOrDefault(u => u.Id == id);
                if ( data is null || data.IsDeleted )
                {
                    result.ExceptionMessage = $"User with id: {id} is not found";
                    return result;
                }
                data.IsDeleted = true;
                data.IsActive = false;
                //service.User.Remove(data);
                service.SaveChanges();
                result.Entity = mapper.Map<UserViewModel>(data);
                result.IsSuccess = true;
            }
            return result;
        }
    }
}
