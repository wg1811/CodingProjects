using System;
using System.IO;
using System.Text.Json; // 1. Needed to do json stuff
using System.Text.RegularExpressions;

namespace StudentManagerApp
{
    public class StudentController
    {
        private List<Student> students = new List<Student>();
        private StudentView view = new();
        public int nextId = 0;
        private string filePath = "students.json"; // 2. Add file path

        public StudentController()
        {
            LoadFromFile(); // 3. Method to handle the file created way below.
        }

        //  Student CRUD stuff
        public void AddStudent()
        {
            Console.WriteLine("\nPlease enter your first name: ");
            string firstName = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter your last name: ");
            string lastName = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter your last age: ");
            int age = CheckIfAge(Console.ReadLine());

            Console.WriteLine("\nPlease enter your email: ");
            string? email = (Console.ReadLine()) ?? string.Empty;

            students.Add(
                new Student
                {
                    Id = nextId++,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Email = email,
                }
            );

            SaveToFile(); // 4. Have to save so it persists (made below)

            Console.WriteLine("Student entered successfully.");
        }

        public void ShowStudent()
        {
            Console.WriteLine("Please enter the student id.");
            int studentId = CheckIfId(Console.ReadLine());
            var existingStudent = students.Find(s => s.Id == studentId);
            if (existingStudent == null)
            {
                Console.WriteLine("Student doesn't exist");
            }
            else
            {
                view.DisplayStudent(existingStudent);
            }
        }

        public void ShowStudents()
        {
            view.DisplayStudents(students);
        }

        public void DeleteStudent()
        {
            Console.WriteLine("Please enter the student id.");
            int studentId = CheckIfId(Console.ReadLine());
            var existingStudent = students.Find(s => s.Id == studentId);
            if (existingStudent == null)
            {
                Console.WriteLine("Student doesn't exist");
            }
            else
            {
                students.Remove(existingStudent);
                SaveToFile();
                Console.WriteLine("Student successfully deleted.");
            }
        }

        public void UpdateStudent()
        {
            Console.WriteLine("Enter Student Id to Update");
            int id = CheckIfId(Console.ReadLine());
            var existingStudent = students.Find(s => s.Id == id);
            if (existingStudent == null)
            {
                Console.WriteLine("Student is not found");
            }
            else
            {
                Console.WriteLine("\nPlease enter student's first name: ");
                string firstName = CheckIfString(Console.ReadLine()) ?? existingStudent.FirstName;

                Console.WriteLine("\nPlease enter student's last name: ");
                string lastName = CheckIfString(Console.ReadLine()) ?? existingStudent.LastName;

                Console.WriteLine("\nPlease enter student's age: ");
                int age = CheckIfAge(Console.ReadLine());

                Console.WriteLine("\nPlease enter your email: ");
                string? email = (Console.ReadLine()) ?? existingStudent.Email;

                //update existed Student
                existingStudent.FirstName = firstName;
                existingStudent.LastName = lastName;
                existingStudent.Age = age;
                existingStudent.Email = email;

                SaveToFile(); //save changing
                Console.WriteLine("student successfully updated");
            }
        }

        // Function to check if the input is a valid string (only letters)
        public static string CheckIfString(string? input)
        {
            string name = input ?? string.Empty;
            while (true)
            {
                if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter only letters.");
                    name = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        // Function to check if the input is a valid integer within the valid age range
        public static int CheckIfAge(string? input)
        {
            string keyInput = input ?? string.Empty;
            while (true)
            {
                if (int.TryParse(keyInput, out int age))
                {
                    if (age >= 0 && age <= 126)
                    {
                        return age;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter a valid age.");
                        keyInput = Console.ReadLine() ?? string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid age.");
                    keyInput = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        // Function to check if the input is a valid Id
        public static int CheckIfId(string? input)
        {
            string keyInput = input ?? string.Empty;
            while (true)
            {
                if (int.TryParse(keyInput, out int studentId))
                {
                    if (studentId >= 0)
                    {
                        return studentId;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter a valid Id.");
                        keyInput = Console.ReadLine() ?? string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid Id.");
                    keyInput = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        // Load File stuff
        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                students =
                    JsonSerializer.Deserialize<List<Student>>(jsonData) ?? new List<Student>();

                if (students.Count > 0)
                {
                    nextId = students.Max(s => s.Id) + 1;
                }
            }
        }

        private void SaveToFile()
        {
            string jsonData = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText(filePath, jsonData);
        }
    }
}
