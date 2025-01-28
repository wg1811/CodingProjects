using System;

namespace Hangman1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RandomWord randomWord = new RandomWord();
            randomWord.FillList("quality");
            string secretWord = randomWord.GetWord();

            //secretWord = "quality";
            secretWord = secretWord.ToUpper(); //.ToUpperInvariant();
            int userRound = secretWord.Length * 2;

            //create a char array from secret word
            char[] secretWordCharArray = secretWord.ToCharArray();
            //list for found letters
            // - - - - - - -
            List<char> foundLetters = new List<char>(new string('-', secretWord.Length));
            // console out foundletters array
            Console.WriteLine(string.Join(" ", foundLetters));
            while (true)
            {
                Console.WriteLine("Enter a letter");
                char userInput = char.ToUpper(Console.ReadKey().KeyChar);

                Console.WriteLine("user input is " + userInput);
                int counter = 0;

                // to compare my char with secret word char
                // i create a foreach loop
                foreach (char c in secretWordCharArray)
                {
                    if (c == userInput)
                    {
                        foundLetters[counter] = c;
                    }
                    counter++;
                }

                // console out foundletters array
                Console.WriteLine(string.Join(" ", foundLetters));
                userRound--;
                Console.WriteLine(userRound);
                if (userRound == 0)
                {
                    Console.WriteLine("Game Over");
                    break;
                }
            }
        }
    }
}








// 1. Make list of hidden words to randomly choose from
// 2. Select random word
// 3. Make string into char array
// 4. Show empty spaces to user
// 5. Get user input
// 6. Compare to word
// 7. Show results
