using System;
using System.Collections.Generic;
using System.Text;

namespace Millionair
{
    public class Answer

        {
            public Answer(string answerText)
            {
           
             answerText = answerText.Substring(1).Trim();
             this.answerText = answerText;
            }

       
        private string answerText;
        public string AnswerText { get { return answerText; } }
        public void ShowAnswer()
        {
           
            Console.WriteLine(answerText);
        }

    }

}
