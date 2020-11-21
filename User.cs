using System;
using System.Collections.Generic;
using System.Text;

namespace Millionair
{
    class User
    {
        public User(string name, int totalScore = 0)
        { 
          this.name = name;
          this.score = new Score(totalScore);
        }
        private string name;
        public string Name { get { return this.name; } }

        private Score score;
        public Score Score { get { return score; } }


       
        
        public static string UserInputName(string text)
        {
            Console.WriteLine(text);
            string Name = Console.ReadLine();
            return Name;
        }
        public  int UserInputNumber(string text)
        {
            Console.WriteLine(text);
            string number = Console.ReadLine();
            try
            {
                int InputNumber = int.Parse(number);
                if (InputNumber < 5 && InputNumber > 0)
                {
                    return InputNumber;
                }
                else
                {
                    Console.WriteLine("Можно вводить только числа 1, 2, 3, 4");
                    return UserInputNumber(text);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Можно вводить только числа 1, 2, 3, 4");
                return UserInputNumber(text);
            }
        }
        

    }
}
   
