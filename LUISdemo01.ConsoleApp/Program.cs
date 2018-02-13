using System;

namespace LUISdemo01.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting searching...");

            Request();

            Console.ReadLine();
        }

        static async void Request()
        {
            using (var luis = new LuisClient())
            {
                Console.Write("Fai la tua domanda: ");
                string query = Console.ReadLine();
                string response = await luis.SearchAsync(query);

                Console.WriteLine(response);
            }
        }
    }
}
