

class Student
{
    public int Id { get; set; } = 0;
    public string Name {get; set; } = "";
    public DateTime BirthDay { get; set; } = new DateTime();
    public Dictionary<string, char> Courses { get; set; } = new Dictionary<string, char>();

    public Student() {}

}
