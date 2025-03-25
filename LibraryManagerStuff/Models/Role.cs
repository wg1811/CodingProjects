using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Role // I'm not sure how roles work with Admin and Librarian.
// I'm screwing this up.  What happens when a user is an Admin, Librarian, and User?
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Admin")]
    public int AdminId { get; set; }
    public Admin? Admin;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public const string User = "User";

    [ForeignKey("Librarian")]
    public int LibrarianId { get; set; }
    public Librarian? Librarian { get; set; }
}
