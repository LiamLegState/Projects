using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Set s1 = new Set();
            Set s2 = new Set();

            s1.SetMaker(s1.Rng()); // Calling a function that initialises the set using a "random number generator" function as parameters.
            Console.WriteLine($"\n Set 1:{s1.ToString()} \n"); //Printing the set using the returned string values of the bool array indexes.

            s2.Rng();
            s2.SetMaker(s2.Rng());
            Console.WriteLine($" Set 2:{s2.ToString()}");
            Console.WriteLine("\n-----------------------------------------------------");

            s2.Union(s1);
            Console.WriteLine($" Union:\n {s2.ToString()}");
            Console.WriteLine("-----------------------------------------------------");

            s1.Intersect(s2);
            Console.WriteLine($" Intersection:\n {s1.ToString()}");
            Console.WriteLine("-----------------------------------------------------");

            Console.WriteLine("\nPlease enter 3 numbers between 0~1000:"); //User set
            Set s3 = new Set();
            int.TryParse(Console.ReadLine(), out int num1);
            int.TryParse(Console.ReadLine(), out int num2);
            int.TryParse(Console.ReadLine(), out int num3);

            if ((num1 >= 0 && num1 <= 1000) && (num2 >= 0 && num2 <= 1000) && (num3 >= 0 && num3 <= 1000))
            {
                s3.Insert(num1);
                s3.Insert(num2);
                s3.Insert(num3);
                Console.WriteLine("\nIs the entered set a subset of s1 - " + s3.IsSubset(s1));
                Console.WriteLine("\n-----------------------------------------------------");
            }

            else
            {
                Console.WriteLine("\nYou've entered invalid numbers.");
                Console.WriteLine("\n-----------------------------------------------------");
            }

            Console.WriteLine("\nPlease enter a number between 0~1000: ");
            int.TryParse(Console.ReadLine(), out int num4);
            if (num4 >= 0 && num4 <= 1000)
            {
                if (s1.IsMember(num4) == true)
                { Console.WriteLine("\nThe number is a member of s1."); }
                else { Console.WriteLine("\nThe number is not a member of s1."); }

                if (s3.IsMember(num4) == true)
                { Console.WriteLine("The number is a member of s2."); }
                else { Console.WriteLine("The number is not a member of s2."); }

                if (s3.IsMember(num4) == true)
                { Console.WriteLine("The number is a member of s3."); }
                else { Console.WriteLine("The number is not a member of s3."); }
                Console.WriteLine("\n-----------------------------------------------------");
            }

            Console.WriteLine("\nPlease enter a number between 0~1000: ");
            int.TryParse(Console.ReadLine(), out int num5);
            if (num5 >= 0 && num5 <= 1000)
            {
                s1.Insert(num5);
                s2.Insert(num5);
                s3.Insert(num5);
                Console.WriteLine($"\nInsertion:\n \nSet 1: {s1.ToString()}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"Set 2: {s2.ToString()}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"Set 3: {s3.ToString()}");
            }

            Console.WriteLine("\nPlease enter a number you wish to be deleted from the set");
            int.TryParse(Console.ReadLine(), out int num6);
            if (num5 >= 0 && num5 <= 1000)
            {
                s1.Delete(num6);
                s2.Delete(num6);
                s3.Delete(num6);
                Console.WriteLine($"\nDeletion: \n Set 1: {s1.ToString()}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"Set 2: {s2.ToString()}");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"Set 3: {s3.ToString()}");
            }

        }
    }
}
