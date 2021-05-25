using System.Collections.Generic;

namespace chatModel
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string UserName { get; set; }
        public bool? Status { get; set; }
        public int? UserAddressId { get; set; }
        public List<UsersUserTypes> UserTypes { get; set; }
    }
}
