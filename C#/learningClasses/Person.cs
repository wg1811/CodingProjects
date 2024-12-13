using System.Security.Cryptography.X509Certificates;

namespace learningClasses
{
    class Person
    {
        //properties
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public bool HasPet { get; set; } = false;
        public bool HasCar { get; set; } = true;
        public string City { get; set; } = "";

        //constructor
        public Person()
        {
            //empty on purpose
        }

        public void PersonInfo()
        {
            string personInfo = "";
            if (HasPet)
            {
                personInfo = $@"
            {Name} is {Age}.  
            They live in {City}.
            {Name} has a pet.
            ";
            }
            else
            {
                personInfo = $@"
            {Name} is {Age}.  
            They live in {City}.
            {Name} doesn't have a pet.
            ";
            }
            Console.WriteLine(personInfo);
        }
    }

    class Car
    {
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; } = 0;
        public bool FourWheelDrive { get; set; } = true;
        public string Color { get; set; } = "";

        public Car()
        { }

        public void ShowCarInfo()
        {
            string carInfo = "";
            if (FourWheelDrive)
            {
                carInfo = $@"
            The {Color} {Year} {Make} {Model} is for sale.
            It has four wheel drive.
            ";
            }
            else
            {
                carInfo = $@"
            The {Color} {Year} {Make} {Model} is for sale.
            It doesn't have four wheel drive.
            ";
            }
            Console.WriteLine(carInfo);
        }
    }

    class Student
    {
        //properties
        public string Name {get; set;} = "";
        public int Age {get; set;} = 0;

        //constructor
        public Student()
        {

        }

        public void DisplayStudent()
        {
            string name = this.Name;
            int age = this.Age;

            Console.WriteLine($"-----------------------------\n\n{name} is {age} years old.\n\n-----------------------------");
        }
    } //end of Student class

    class Classroom
    {
        //properties
        public List<Student>? StudentList {get; set;}
        public string Name {get; set;} = "";

        public string Semester {get;set;} = "";

        //constructor
        public Classroom()
        {

        }

        public void DisplayStudents() {
            foreach (Student student in StudentList) {
                Console.WriteLine(student.Name + " is " + student.Age);
            }
        }

        public void DisplayClassInfo() {
            string name = this.Name;
            string semester = this.Semester;
            List<Student>? studentList = this.StudentList;

            Console.WriteLine($"-----------------------------\n\n{name} takes place in {semester}.\n Student List: ");
            DisplayStudents();
            Console.WriteLine("\n\n-----------------------------");
        }
    }
}
