using System;
using System.ComponentModel.DataAnnotations;
namespace Emerce_Model.User
{
    public class UserUpdateModel
    {
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        public string Username { get; set; }

        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        public string Password { get; set; }
        public DateTime? Udatetime { get; set; }
        public int? Uuser { get; set; }
    }
}