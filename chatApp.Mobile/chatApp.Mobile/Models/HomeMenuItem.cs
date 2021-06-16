namespace chatApp.Mobile.Models
{
    public enum MenuItemType
    {
        Welcome,
        AboutUs,
        Settings, 
        Profile,
        Logout,
        Feedback
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}