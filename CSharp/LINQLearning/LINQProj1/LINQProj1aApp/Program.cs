using System;
using System.Collections.Generic;

namespace LINGProj1App
{
    // Define the Student class first
    public class Student
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    internal class Program
    {
        // Move arrays to class level and make them private
        private static readonly string[] names = new string[]
        {
            "Alice",
            "Bob",
            "Charlie",
            "David",
            "Emma",
            "Frank",
            "Grace",
            "Henry",
            "Isabella",
            "Jack",
            "Katie",
            "Liam",
            "Mia",
            "Noah",
            "Olivia",
            "Paul",
            "Quinn",
            "Ryan",
            "Sophia",
            "Tom",
            "Ursula",
            "Victor",
            "Wendy",
            "Xavier",
            "Yasmine",
            "Zachary",
            "Abigail",
            "Benjamin",
            "Charlotte",
            "Daniel",
            "Eleanor",
            "Felix",
            "Gabriella",
            "Harvey",
            "Ivy",
            "James",
            "Katherine",
            "Lucas",
            "Madeline",
            "Nathan",
            "Oscar",
            "Penelope",
            "Richard",
            "Samuel",
            "Theodore",
            "Uma",
            "Vincent",
            "William",
            "Xander",
            "Yvonne",
            "Zoe",
        };

        private static readonly int[] nums = new int[]
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18,
            19,
            20,
            21,
            22,
            23,
            24,
            25,
            26,
            27,
            28,
            29,
            30,
            31,
            32,
            33,
            34,
            35,
            36,
            37,
            38,
            39,
            40,
            41,
            42,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51,
            52,
            53,
            54,
            55,
        };

        public static void Main(string[] args)
        {
            List<Student> students = CreatingStudentList();
            // The list is already printed in the method, but you could add additional logic here if needed
        }

        public static List<Student> CreatingStudentList()
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < 20; i++)
            {
                Student newStudent = new Student { Name = names[i], Age = nums[10 + i] };
                students.Add(newStudent);
            }

            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} is {student.Age}");
            }
            return students;
        }
    }
}
