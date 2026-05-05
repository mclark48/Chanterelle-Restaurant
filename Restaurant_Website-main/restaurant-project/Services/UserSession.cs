using restaurant_project.Models; // used this for verification but am scared to delete -@mclark48

namespace restaurant_project.Services;

//this class shows loggedin vs loggedout status. -@mclark48
public class UserSession
{
    public User? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}