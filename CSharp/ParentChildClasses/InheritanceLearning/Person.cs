namespace InheritanceLearning
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;

        public Person() { }

        // Display info, but can only display properties in Parent class. Uses 'virtual' so it can be
        // overridden in child classes.  This is an example of polymorphism:
        // Supports polymorphism, meaning the method to execute is determined at runtime based on the actual type of the object.
        public virtual void DisplayInfo()
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

        public Staff() { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Hi {Name}, you're {Age} years old?  And you're a {Position}?");
        }
    }

    public class TruckDriver : Person
    {
        public string TruckType { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public DateTime LicenseExpires { get; set; }
    }
}
