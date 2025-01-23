namespace InheritanceLearning
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;

        public void DisplayInfo()
        {
            Console.WriteLine(
                $"Hi {Name}, you're {Age} years old?  You look awful.  Do you smoke?"
            );
        }
    }

    public class Staff : Person
    {
        public int Id { get; set; }
        public double Salary { get; set; }
        public string Position { get; set; } = "";
    }

    public class TruckDriver : Person
    {
        public string TruckType { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public DateTime LicenseExpires { get; set; }
    }
}
