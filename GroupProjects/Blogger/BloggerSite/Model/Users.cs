using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//
public enum UserRole
{
    Admin,
    Writer,
    Reader,
}

#nullable disable
public class User
{
    [Key]
    public int Id { get; set; }

    public UserRole UserRole { get; set; } = UserRole.Reader;

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    //one to many
    [JsonIgnore]
    public List<Blog> Blogs { get; set; } = new List<Blog>();
}
