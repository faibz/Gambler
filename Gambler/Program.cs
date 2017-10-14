using System;

namespace RoulettePlayer
{
    internal class Program
    {
        private static void Main()
        {
            var gambler = new Gambler(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));

            while (!gambler.Play())
            {
                Console.WriteLine("Lost a game.");
                gambler.TopUp(1000);
            }

            Console.WriteLine("Won a game.");

            Console.Read();
        }
    }
}
