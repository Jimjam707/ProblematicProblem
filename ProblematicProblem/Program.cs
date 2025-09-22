using System;
using System.Collections.Generic;
using System.Threading;


namespace ProblematicProblem
{
    public class Program
    {
        static Random rng = new Random();
        static bool cont = true;
        static List<string> activities = new List<string>() { "Movies", "Paintball", "Bowling", "Lazer Tag", "LAN Party", "Hiking", "Axe Throwing", "Wine Tasting" };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to the random activity generator! Would you like to generate a random activity? Type 'yes' or 'no': ");
            Console.WriteLine();
            cont = ReadBoolYesNo();
            if (!cont) return;

            Console.WriteLine();
            Console.WriteLine("We are going to need your information first! What is your name? ");
            string userName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine();
            Console.WriteLine("What is your age? ");
            int userAge = ReadIntTryParse();

            Console.WriteLine();
            Console.WriteLine("Would you like to see the current list of activities? yes/no: ");
            bool seeList = ReadBoolYesNo();

            if (seeList)
            {
                PrintActivities();

                Console.WriteLine();
                Console.WriteLine("Would you like to add any activities before we generate one? yes/no: ");
                bool addToList = ReadBoolYesNo();
                Console.WriteLine();

                while (addToList)
                {
                    Console.WriteLine("What would you like to add? ");
                    string? userAddition = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(userAddition))
                    {
                        activities.Add(userAddition.Trim());
                    }

                    PrintActivities();

                    Console.WriteLine();
                    Console.WriteLine("Would you like to add more? yes/no: ");
                    addToList = ReadBoolYesNo();
                    Console.WriteLine();
                }
            }

            while (cont)
            {
                Console.WriteLine("Connecting to the database");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(". ");
                    Thread.Sleep(250);
                }

                Console.WriteLine();
                Console.WriteLine("Choosing your random activity");
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine(". ");
                    Thread.Sleep(250);
                }

                Console.WriteLine();

                if (activities.Count == 0)
                {
                    Console.WriteLine("No activities available. Exiting.");
                    return;
                }

                int randomNumber = rng.Next(activities.Count);
                string randomActivity = activities[randomNumber];

                if (userAge < 21 && string.Equals(randomActivity, "Wine Tasting", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Oh no! Looks like you are too young to do {randomActivity}");
                    Console.WriteLine("Picking something else!");
                    activities.RemoveAll(a => string.Equals(a, "Wine Tasting", StringComparison.OrdinalIgnoreCase));
                    if (activities.Count == 0)
                    {
                        Console.WriteLine("No other activities available. Exiting.");
                        return;
                    }
                    randomNumber = rng.Next(activities.Count);
                    randomActivity = activities[randomNumber];
                }

                Console.WriteLine($"Ah got it, {userName}! Your random activity is: {randomActivity}. Keep or redo? keep/redo: ");
                Console.WriteLine();
                cont = ReadKeepRedoIsRedo();
            }
        }

        static void PrintActivities()
        {
            Console.WriteLine();
            Console.WriteLine("Current activities: ");
            foreach (string activity in activities)
            {
                Console.WriteLine($"{activity} ");
                Thread.Sleep(150);
            }
            Console.WriteLine();
        }

        // Accepts yes/no and also true/false. Internally normalizes then uses TryParse for robustness.
        static bool ReadBoolYesNo()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter yes or no: ");
                    continue;
                }

                input = input.Trim().ToLowerInvariant();
                // Normalize common yes/no to booleans
                if (input is "y" or "yes") return true;
                if (input is "n" or "no") return false;

                // Fallback to bool.TryParse for true/false
                if (bool.TryParse(input, out bool value)) return value;

                Console.WriteLine("Please enter yes or no: ");
            }
        }

        static int ReadIntTryParse()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value)) return value;
                Console.WriteLine("Please enter a valid number: ");
            }
        }

        // Returns true if user wants to redo (continue loop), false if keep (break loop)
        static bool ReadKeepRedoIsRedo()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter keep or redo: ");
                    continue;
                }

                input = input.Trim().ToLowerInvariant();
                if (input is "keep" or "k" or "yes" or "y") return false; // stop looping
                if (input is "redo" or "r" or "no" or "n") return true;   // continue looping

                Console.WriteLine("Please enter keep or redo: ");
            }
        }
    }
}