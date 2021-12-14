using System.ComponentModel.DataAnnotations;
namespace Emerce_Model.User
{
    public class UserCreateModel : IUserModel
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters long.")]
        //[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        //public DateTime Idatetime { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int Iuser { get; set; }
    }
}
