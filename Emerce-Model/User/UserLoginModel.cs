using System.ComponentModel.DataAnnotations;
namespace Emerce_Model.User
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }
    }
}
