using System.Security.Cryptography.X509Certificates;

namespace Oppgaver_uke_1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    
    int arbInt = 7;
    string arbString = "Sandakerveien";
    double arbDouble = 114.01;
    char arbChar = 'k';
    int[] arbIntArray = [1, 23, 43, 4, 6, 7, 4, 85, 3];
    
    for(int i = 0; i > arbIntArray.Length; i++)
    {
        Console.WriteLine(arbIntArray[i]);
    }

    string[] arbStringArray = ["Cat", "Hat", "Bat", "Sat", "Mat", "Nat", "Rat"];
    foreach (string word in arbStringArray)
    {
        Console.WriteLine(word);
    }

    List<string> arbListString = new List<string> {"Joe", "Joakim", "Sigrid"};
    arbListString.Add("Javier");
    arbListString.Add("Jermain");
    arbListString.Add("Kim");
    
    foreach (string word in arbListString)
    {
        Console.WriteLine(word);
    }
    
    //Extra tasks 
    Dictionary<int, string> myDictionary = new Dictionary<int, string>();
    myDictionary.Add(1, "First");
    myDictionary.Add(2, "Second");
    myDictionary.Add(3, "Third");
    myDictionary.Add(4, "Fourth");

    foreach (KeyValuePair<int, string> item in myDictionary)
    {
        Console.WriteLine($"The key is {item.Key} and the value is {item.Value}.");
    }

    Console.WriteLine(myIntAdder(45,55));

    myGreeter("Devrim");

    }

    static public int myIntAdder(int a, int b)
    {
        return a + b;
    }

    static public string myGreeter(string personToGreet)
    {
        string greeting = $"Hi {personToGreet}!  I hope you're doing well.";        
        Console.WriteLine(greeting);
        return greeting;
    }
}
