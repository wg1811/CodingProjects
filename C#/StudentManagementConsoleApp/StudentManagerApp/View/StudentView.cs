namespace StudentManagerApp
{
    public class StudentView
    {
        public void DisplayStudent(Student student)
        {
            Console.WriteLine(new string('_', 30));
            Console.WriteLine(
                $"Student id: {student.Id}\nStudent name: {student.FirstName}\nStudent Age: {student.Age}\nStudent Email: {student.Email}"
            );
            Console.WriteLine(new string('_', 30));
        }

        public void DisplayStudents(List<Student> students)
        {
            Console.WriteLine(new string('_', 30));
            foreach (var student in students)
            {
                DisplayStudent(student);
            }
        }
    }
}
