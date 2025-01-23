using System.Reflection.Metadata.Ecma335;

namespace InheritanceLearning
{
    // Create animal class with child.  Properties name, habitat, age, isPredetor, dietType, sound.

    public class Animal
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public string[] Habitat { get; set; } =
            ["Jungle", "Desert", "Forest", "Plains", "Mountain", "Polar"];

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"The animal's name is {Name}\nThe animals's age is {Age}\nThe animal lives in the {Habitat}.");
        }
    }

    public class Snake : Animal
    {
        public double Length {get; set;}
        public Boolean IsVenomous {get; set;}

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"The {Name} is {Length} meters long.");
            Console.WriteLine(IsVenomous ? "It's venomous" : "It's not venomous");
        }
    }

    public class Bear : Animal
    {
        public Boolean IsHibernating { get; set; }
        public string[] BearType { get; set; } =
        ["Polar", "Grizzly", "Black", "Brown"];

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine(IsHibernating ? "It's hibernating" : "It's not hibernating");
        }
    }

    public class Cat : Animal
    {
        public string[] CatType { get; set; } = 
        ["Tiger", "Lion", "Jaguar", "Leapord", "Housecat"];
    }

}

