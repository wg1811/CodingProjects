using System;
using System.Text.RegularExpressions;

namespace CeasorCypher
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Getting Player 1 Inputs
            Console.WriteLine(
                "__________________________\nWelcome to Ceasor Cypher!\nPlease enter your secret word or phrase. \n(No punctuation. Has to be only letters. Not case sensitive)"
            );
            string secretWord = Console.ReadLine() ?? string.Empty;
            while (true)
            {
                if (Regex.IsMatch(secretWord, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("You entered: " + secretWord);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter only letters.");
                    secretWord = Console.ReadLine() ?? string.Empty;
                }
            }
            Console.WriteLine(
                "__________________________\nPlease enter your secret key to encrypt your word. (Must be an integer from 0 to 26.)"
            );
            string keyInput = Console.ReadLine() ?? string.Empty;
            int secretKey = 0;
            bool isLetter = false;
            while (!isLetter)
            {
                if (int.TryParse(keyInput, out secretKey))
                {
                    if (secretKey >= 0 && secretKey <= 26)
                    {
                        Console.WriteLine($"You entered: {secretKey}");
                        isLetter = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter a valid number.");
                        keyInput = Console.ReadLine() ?? string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                    keyInput = Console.ReadLine() ?? string.Empty;
                }
            }

            // Encrypting secretWord
            char[] secretWordCharArray = secretWord.ToLower().ToArray();
            char[] charSecret = CeasarEncryption(secretWordCharArray, secretKey);
            Console.WriteLine("________\nThe encrypted word is: ");
            foreach (char c in charSecret)
            {
                Console.Write(c);
            }
            while (true)
            {
                Console.WriteLine("\nPlease hit y before passing to player 2.");
                string passKeyInput = Console.ReadLine() ?? string.Empty;
                if (passKeyInput == "y")
                {
                    Console.WriteLine(
                        "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nReady To Play?\n\n_________________________"
                    );
                    break;
                }
            }

            // Player 2 Prompts
            bool won = false;
            Console.WriteLine("Guess the key (between 1 and 26):");
            int player2Key;
            int guessCounter = 0;
            while (!won)
            {
                string input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out player2Key))
                {
                    if (player2Key < 0 || player2Key > 26)
                    {
                        Console.WriteLine("Please enter a number from 0 to 26.");
                        guessCounter++;
                    }
                    else if (player2Key < secretKey)
                    {
                        Console.WriteLine("Too low! Try again.");
                        guessCounter++;
                    }
                    else if (player2Key > secretKey)
                    {
                        Console.WriteLine("Too high! Try again.");
                        guessCounter++;
                    }
                    else
                    {
                        guessCounter++;
                        won = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }

            // Win / Lose Message
            if (won)
            {
                Console.WriteLine(
                    $"_________________________________\n\nCongratulations! You won!\nThe secret key was: {secretKey}.\nThe secret word was: {secretWord}."
                );
            }
            else
            {
                Console.WriteLine(
                    $"\n_________________________________\n\nOh no! You suck! You lost!\nThe secret key was: {secretKey}.\nThe secret word was: {secretWord}.\nBe less bad at things next time! :)"
                );
            }
        }

        // Function to encrypt
        public static char[] CeasarEncryption(char[] input, int key)
        {
            List<char> dynamicList = new List<char>();
            foreach (char element in input)
            {
                int letter = ((element - 97) + key) % 26;
                char charLetter = (char)(letter + 97);
                dynamicList.Add(charLetter);
            }
            char[] outputArray = dynamicList.ToArray();
            return outputArray;
        }
    }
}
