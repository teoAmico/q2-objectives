using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var overview = new CSharpOverview();
            overview.ShowCSharpExamples();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

        }
    }

    public class CSharpOverview
    {
        public void ShowCSharpExamples()
        {
            Console.WriteLine("C# Examples");

            // Data Types
            int intValue = 42;
            double doubleValue = 3.14159;
            bool boolValue = true;
            char charValue = 'A';
            string stringValue = "Hello, World!";
            object objectValue = new { Name = "Alice", Age = 30 };

            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"double: {doubleValue}");
            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"string: {stringValue}");
            Console.WriteLine($"object: {objectValue}");

            // Math Operations
            int a = 5, b = 3;
            Console.WriteLine($"Addition: {a} + {b} = {a + b}");
            Console.WriteLine($"Subtraction: {a} - {b} = {a - b}");
            Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
            Console.WriteLine($"Division: {a} / {b} = {a / b}");
            Console.WriteLine($"Modulus: {a} % {b} = {a % b}");
            Console.WriteLine($"Power: {Math.Pow(a, b)}");
            Console.WriteLine($"Square Root: {Math.Sqrt(a)}");

            // Control Structures
            if (a > b) Console.WriteLine($"{a} is greater than {b}");
            switch (a)
            {
                case 1: Console.WriteLine("One"); break;
                case 5: Console.WriteLine("Five"); break;
                default: Console.WriteLine("Other number"); break;
            }
            for (int i = 0; i < 3; i++) Console.WriteLine($"for loop: {i}");
            int[] numbers = { 1, 2, 3 };
            foreach (var num in numbers) Console.WriteLine($"foreach loop: {num}");

            // OOP Concepts
            var person = new Person { Name = "Bob", Age = 25 };
            Console.WriteLine($"Person: {person.Name}, {person.Age}");

            // Collections
            var list = new List<int> { 1, 2, 3 };
            Console.WriteLine($"List: {string.Join(", ", list)}");
            var dict = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } };
            Console.WriteLine($"Dictionary: {string.Join(", ", dict.Select(kv => kv.Key + "=" + kv.Value))}");

            // LINQ
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"Even numbers using LINQ: {string.Join(", ", evenNumbers)}");

            // Exception
            try
            {
                int x = 0;
                int y = 10 / x;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }

            // Asynchronous Programming
            var task = ExampleAsync();
            task.Wait();

            // file
            var filePath = "example.txt";
            System.IO.File.WriteAllText(filePath, "Hello, File!");
            var fileContent = System.IO.File.ReadAllText(filePath);
            Console.WriteLine($"File content: {fileContent}");

            var calculator = new Calculator();
            calculator.Add(1, 2);

            var eventExample = new EventExample();
            eventExample.TriggerEvent();
        }

        private async Task ExampleAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Delayed for 1 second");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Calculator
    {
        public event Action OnCalculate;

        public int Add(int x, int y)
        {
            OnCalculate?.Invoke();
            return x + y;
        }
    }

    public class EventExample
    {
        public event Action ExampleEvent;

        public EventExample()
        {
            ExampleEvent += () => Console.WriteLine("Event triggered!");
        }

        public void TriggerEvent()
        {
            ExampleEvent?.Invoke();
        }
    }
}
