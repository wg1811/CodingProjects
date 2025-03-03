using System;

namespace SpaceshipConApp
{
    public class Spaceship
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public double? Fuel { get; set; }
        public bool HasPilot { get; set; }
        public string? Destination { get; set; }
        public string? NewDestination { get; set; }

        public Spaceship(string name, string color, double fuel, bool hasPilot, string destination)
        {
            Name = name;
            Color = color;
            Fuel = fuel;
            HasPilot = hasPilot;
            Destination = destination;
            NewDestination = "";
        }

        public void BuildSpaceship()
        {
            Console.WriteLine("What color spaceship would you like, Admiral?");
            Color = Console.ReadLine();
            Console.WriteLine("What would you like to name your spaceship, Admiral?");
            Name = Console.ReadLine();
            Console.WriteLine($"Spaceship {Name} is {Color} has been built.");
        }

        public bool IgniteOn()
        {
            if (Fuel == 100 && HasPilot && NewDestination != Destination)
            {
                Console.WriteLine("Spaceship is ready to take off!");
                return true;
            }
            else
            {
                Console.WriteLine("Spaceship is not ready to take off!");
                return false;
            }
        }

        public void ShowStatus()
        {
            if (Name == "" || Color == "" || Fuel == null || Destination == "")
            {
                Console.WriteLine("Spaceship has not been built yet.");
                return;
            }
            Console.WriteLine($"Spaceship {Name} is {Color} and has {Fuel}% fuel left.");
            if (HasPilot == true)
            {
                Console.WriteLine("Spaceship has a pilot.");
            }
            else
            {
                Console.WriteLine("Spaceship does not have a pilot.");
            }
            Console.WriteLine($"Spaceship is heading to {Destination}.");
        }

        public void Refuel()
        {
            if (Name == "" || Color == "" || Fuel == null || Destination == "")
            {
                Console.WriteLine("Spaceship has not been built yet.");
                return;
            }
            Fuel = 100;
            Console.WriteLine("Spaceship has been refueled.");
        }

        public void ChangeDestination()
        {
            Console.WriteLine("Where are we headed, Admiral?");
            NewDestination = Console.ReadLine();
            Console.WriteLine($"Spaceship is now heading to {NewDestination}.");
        }

        public void BoardPilot()
        {
            if (Name == "" || Color == "" || Fuel == null || Destination == "")
            {
                Console.WriteLine("Spaceship has not been built yet.");
                return;
            }
            HasPilot = true;
            Console.WriteLine("Pilot has boarded the spaceship.");
        }

        public void DisembarkPilot()
        {
            HasPilot = false;
            Console.WriteLine("Pilot has disembarked the spaceship.");
        }

        public void Land()
        {
            Console.WriteLine("Spaceship has landed.");
        }

        public void Launch()
        {
            if (Name == "" || Color == "" || Fuel == null || Destination == "")
            {
                Console.WriteLine("Spaceship has not been built yet.");
                return;
            }
            bool checkReady = IgniteOn();
            if (checkReady)
            {
                Console.WriteLine("Spaceship has launched.");
                Console.WriteLine("That was quick, we're here.");
                Land();
                DisembarkPilot();
                Fuel = 0;
                Destination = NewDestination;
            }
        }
    }
}
