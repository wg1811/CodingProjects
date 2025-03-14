using System;

namespace LINGProj1App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string[] names = new string[]
            {
                "Alice",
                "Bob",
                "Charlie",
                "David",
                "Emma",
                "Frank",
                "Grace",
                "Henry",
                "Isabella",
                "Jack",
                "Katie",
                "Liam",
                "Mia",
                "Noah",
                "Olivia",
                "Paul",
                "Quinn",
                "Ryan",
                "Sophia",
                "Tom",
                "Ursula",
                "Victor",
                "Wendy",
                "Xavier",
                "Yasmine",
                "Zachary",
                "Abigail",
                "Benjamin",
                "Charlotte",
                "Daniel",
                "Eleanor",
                "Felix",
                "Gabriella",
                "Harvey",
                "Ivy",
                "James",
                "Katherine",
                "Lucas",
                "Madeline",
                "Nathan",
                "Oscar",
                "Penelope",
                "Richard",
                "Samuel",
                "Theodore",
                "Uma",
                "Vincent",
                "William",
                "Xander",
                "Yvonne",
                "Zoe",
            };

            int[] nums = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
                33,
                34,
                35,
                36,
                37,
                38,
                39,
                40,
                41,
                42,
                43,
                44,
                45,
                46,
                47,
                48,
                49,
                50,
                51,
                52,
                53,
                54,
                55,
            };

            var intQuery = from num in nums where num < 15 && num > 4 select num;

            foreach (var num in intQuery)
            {
                Console.Write(num + ",");
            }
        }
    }
}
