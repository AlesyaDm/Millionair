using System;
using System.Text;

namespace Millionair
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
                               
            Game game = new Game();
            game.Start();

           
        }

    }
}
