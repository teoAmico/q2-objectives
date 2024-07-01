using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialTestCMD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
            {
                Console.WriteLine($"Factorial of {number} is {Factorial(number)}");
            }
            else
            {
                Console.WriteLine("enter non negative namber");
            }
        }

        public static long Factorial(int number)
        {
            if (number > 0)
            {
                return number * (Factorial(number - 1));
            }
            
            return 1;

        }
    }
}
