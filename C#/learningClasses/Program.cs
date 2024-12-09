using System;
namespace learningClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person()
            {
                Name = "Ted",
                Age = 24,
                City = "London",
                HasPet = true,
            };

            person.PersonInfo();

            Car newCar = new Car()
            {
                Make = "Porsche",
                Model = "911",
                Year = 1970,
                Color = "Yellow",
                FourWheelDrive = false,
            };

            newCar.ShowCarInfo();

        }
    }
}