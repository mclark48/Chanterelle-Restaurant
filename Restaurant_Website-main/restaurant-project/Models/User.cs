// sets user class. gives everyone a unique id and email. all in c# -@mclark48

namespace restaurant_project.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
}