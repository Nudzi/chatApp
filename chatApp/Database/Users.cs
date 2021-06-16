using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class Users
    {
        public Users()
        {
            Feedbacks = new HashSet<Feedbacks>();
            FriendsUserIdprimaryNavigation = new HashSet<Friends>();
            FriendsUserIdsecondaryNavigation = new HashSet<Friends>();
            HistoriesUserIdPrimaryNavigation = new HashSet<Histories>();
            HistoriesUserIdSecondaryNavigation = new HashSet<Histories>();
            UserImages = new HashSet<UserImages>();
            UsersUserTypes = new HashSet<UsersUserTypes>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool? Status { get; set; }
        public int? UserAdressId { get; set; }

        public virtual UserAdresses UserAdress { get; set; }
        public virtual ICollection<Feedbacks> Feedbacks { get; set; }
        public virtual ICollection<Friends> FriendsUserIdprimaryNavigation { get; set; }
        public virtual ICollection<Friends> FriendsUserIdsecondaryNavigation { get; set; }
        public virtual ICollection<Histories> HistoriesUserIdPrimaryNavigation { get; set; }
        public virtual ICollection<Histories> HistoriesUserIdSecondaryNavigation { get; set; }
        public virtual ICollection<UserImages> UserImages { get; set; }
        public virtual ICollection<UsersUserTypes> UsersUserTypes { get; set; }
    }
}
