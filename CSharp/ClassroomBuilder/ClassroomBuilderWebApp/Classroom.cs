

using System.Data.SqlTypes;

class Classroom
{
    public string Name { get; set; } = "";
    public List<Student> students = new List<Student>();
    public Classroom() {}

    //add new student

    public string AddStudent(Student student)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);

        if (existingStudent == null)
        {
            return $"Student with id {student.Id} already exists.";
        }

        students.Add(student);
        return $"Student with name {student.Name} added with Id: {student.Id}.";
    }

    //Remove student
    public string Removestudent(int studentId)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == studentId);

        if (existingStudent == null)
        {
            return $"Student with id {studentId} doesn't exist.";
        }

        students.Remove(existingStudent);
        return $"Student with Id: {studentId} named {existingStudent.Name} has been removed.";
    }
    //Find student by ID
    public string FindStudentById(int studentId)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == studentId);

        if (existingStudent == null)
        {
            return $"Student with id {studentId} not found.";
        }

        return $"Student with name {existingStudent.Name} and Id: {studentId} is found.";
    }

    //Find by Name
    public string FindStudentByName(string studentName)
    {
        var matchingStudent = students.Where(s => s.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingStudent.Count == 0)
        {
            return $"Student named {studentName} not found.";
        }

        return $"Found {matchingStudent.Count} student(s) with name {studentName}." +
        string.Join(", ", matchingStudent.Select(s => $"Id: {s.Id} Name: {s.Name}"));
    }

    public string UpdateDetails(int studentId, string newName)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);

        if (student == null)
        {
            return $"Student with id {studentId} not found.";
        }

        student.Name = newName;
        return $"Student with ID {studentId} updated. New Name: {newName}.";
    }

}
