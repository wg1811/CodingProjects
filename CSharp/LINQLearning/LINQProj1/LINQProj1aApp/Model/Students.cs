public class Students
{
    public List<Student>? StudentList { get; set; }

    public Students()
    {
        StudentList = new List<Student>();
    }

    public List<Student> namesStartingWithX(List<Student> checkThisList)
    {
        List<Student> kidsXNames = new List<Student>();
        var xQuery =
            from xStudents in checkThisList
            where xStudents.Name != null && xStudents.Name.StartsWith("X")
            select xStudents;

        foreach (var kid in xQuery)
        {
            Console.Write(kid + ",");
            kidsXNames.Add(kid);
        }
        return kidsXNames;
        //return StudentList.Where(s => s.Name.StartsWith("X")).Select(s => s.Name).ToArray();
    }
}
