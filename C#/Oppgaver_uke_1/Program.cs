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
    
    }

    
}
