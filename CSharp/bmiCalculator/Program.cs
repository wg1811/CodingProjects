//Get weight and height from console. Calculate BMI weight over height squared. 

using System;
namespace bmiCalculator
{

    internal class Program
    {

        // Methods
        public static void DisplayInfo(Person measuredUser)
        {
            Console.WriteLine($"The person's name is {measuredUser.name}. Their height in cms is {measuredUser.heightInCM}. Their weight in KG is {measuredUser.weightInKG}.");
            Console.WriteLine($"The person's BMI is {measuredUser.bmi}. This means their BMI category is {measuredUser.bmiType}.");
        }


        public static void GetBMI(Person toBeMeasured)
        {
            // Calculate BMI
            double BMI = toBeMeasured.weightInKG / Math.Pow(toBeMeasured.heightInCM, 2) * 10000;
            toBeMeasured.bmi = Math.Round(BMI, 2);
            Console.WriteLine(Math.Pow(toBeMeasured.heightInCM, 2));
        }

        public static void AssignUserBMI(Person toBeAssigned)
        {
            // Underweight: BMI less than 18.5, Normal weight: BMI between 18.5 and 24.9
            // Overweight: BMI between 25 and 29.9, Obesity (Class 1): BMI between 30 and 34.9
            // Obesity (Class 2): BMI between 35 and 39.9, Severe obesity (Class 3): BMI of 40 or higher
            string[] obesityTypes = ["Underweight", "Normal weight", "Overweight", "Obesity Class 1", "Obesity Class 2", "Severe Obesity Class 3"];
            double[] obesityRangeLimits = [18.5, 25, 30, 35, 40];
            for (int i = 0; i < obesityRangeLimits.Length; i++)
            {
                if (toBeAssigned.bmi < obesityRangeLimits[i])
                {
                    toBeAssigned.bmiType = obesityTypes[i];
                    break;
                }
                else toBeAssigned.bmiType = obesityTypes[5];
            }
        }

        public static Person GetUserInfo()
        {
            // Get user's info
            Console.WriteLine("Please enter your name.");
            string? userName = Console.ReadLine();

            Console.WriteLine("Please enter your weight in kg.");
            double userWeight = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter your height in cm.");
            double userHeight = Convert.ToDouble(Console.ReadLine());

            Person thePerson = new Person(userName, userWeight, userHeight);

            return (thePerson);

        }

        static void Main(string[] args)
        {
            //            Person thePerson = new Person("Bob", 200, 210);
            Person newUser = GetUserInfo();
            GetBMI(newUser);
            AssignUserBMI(newUser);
            DisplayInfo(newUser);
        }
    }
}