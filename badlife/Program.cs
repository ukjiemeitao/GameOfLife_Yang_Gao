using System;
using System.IO;

namespace badlife
{
    /// <summary>
    /// Implements Conway's Game Of Life badly
    /// See ..\..\README.txt for full details.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string all_text;

            using (var input = new StreamReader("sample_input.txt"))
            {
                all_text = input.ReadToEnd();
            }

            try
            {
                GameOfLife gameOfLife = new GameOfLife(new Validator());

                gameOfLife.InitializeWorld(all_text);

                for(int i = 0; i <4 ; i++)
                {
                    gameOfLife.Evolve();
                    gameOfLife.OutputNewWorld();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            Console.ReadLine();
        }

        
    }
}
