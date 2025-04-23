using System;
using System.Diagnostics;

namespace BruteForcePassword
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Brute Force Adgangskode Program (Tester forskellige længder)");
            Console.ResetColor();
            Console.WriteLine("Indtast den adgangskode, der skal findes:");
            string targetPassword = Console.ReadLine();
            Console.WriteLine("\nStarter brute force (fra 1 til 10 tegn)...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool passwordFound = BruteForce(targetPassword, 1, 10); // Test fra længde 1 til 10

            stopwatch.Stop();

            if (passwordFound)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nAdgangskoden '{targetPassword}' blev fundet!");
                Console.ResetColor();
                Console.WriteLine($"Tid brugt: {stopwatch.Elapsed}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAdgangskoden blev ikke fundet inden for de angivne længder.");
                Console.ResetColor();
            }

            Console.WriteLine("\nTryk på en tast for at afslutte.");
            Console.ReadKey();
        }

        static bool BruteForce(string target, int minLength, int maxLength)
        {
            char[] charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?".ToCharArray();

            for (int length = minLength; length <= maxLength; length++)
            {
                int[] indices = new int[length];
                long totalLengthCombinations = (long)Math.Pow(charset.Length, length);
                long lengthCounter = 0;
                long outputInterval = Math.Max(1000, totalLengthCombinations / 100); // Opdater ca. 100 gange pr. længde

                while (true)
                {
                    string currentAttempt = GeneratePassword(charset, indices);
                    lengthCounter++;

                    if (currentAttempt == target)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nFundet adgangskode: {currentAttempt}");
                        Console.ResetColor();
                        return true;
                    }

                    if (lengthCounter % outputInterval == 0)
                    {
                        double progress = (double)lengthCounter / totalLengthCombinations * 100;
                        Console.WriteLine($"Længde: {length}, Fremskridt: {progress:F2}%");
                    }

                    // Opdater indices for næste forsøg
                    int i = length - 1;
                    while (i >= 0)
                    {
                        indices[i]++;
                        if (indices[i] < charset.Length)
                        {
                            break;
                        }
                        indices[i] = 0;
                        i--;
                    }

                    // Hvis alle kombinationer for den aktuelle længde er prøvet
                    if (i < 0)
                    {
                        break; // Gå til næste længde
                    }
                }
            }

            return false; // Adgangskoden blev ikke fundet inden for de angivne længder
        }

        static string GeneratePassword(char[] charset, int[] indices)
        {
            char[] password = new char[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                password[i] = charset[indices[i]];
            }
            return new string(password);
        }
    }
}