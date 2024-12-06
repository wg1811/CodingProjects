namespace bmiCalculator
{
    public class Person //needs to be put outside this class, but still in the namespace.
    {
        //attributes
        public string name;
        public double heightInCM;
        public double weightInKG;
        public double bmi;
        public string bmiType;

        //Constructor
        public Person(string userName, double userWeight, double userHeight)
        {
            name = userName;
            heightInCM = userHeight;
            weightInKG = userWeight;
        }
    }

}