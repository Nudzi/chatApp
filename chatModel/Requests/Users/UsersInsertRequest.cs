using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chatModel.Requests.Users
{
    public class UsersInsertRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        public bool Status { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PasswordConfirmation { get; set; }
        public List<int> UserTypes { get; set; } = new List<int>();
        public int? UserAddressId { get; set; }
    }
}
