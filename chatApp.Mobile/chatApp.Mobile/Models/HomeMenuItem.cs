namespace chatApp.Mobile.Models
{
    public enum MenuItemType
    {
        Welcome,
        AboutUs,
        Settings, 
        Profile,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}