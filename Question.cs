using System;
using System.Collections.Generic;
using System.Text;

namespace Millionair
{
    class Question
    {
        public Question(string questionText, Answer[] answers)
        {
           
            this.questionText = questionText;
            this.answers = answers;
    }
        private string questionText;
        private Answer[] answers;
        public string QuestionText { get { return questionText; } }
        public Answer[] Answers { get { return answers; } }
        

        public void ShowQuestion()
        {
            Console.WriteLine("");
            Console.WriteLine("Вопрос:");
            Console.WriteLine(questionText);
            Console.WriteLine("");
            Console.WriteLine("Варианты ответа:");
            for (int i = 0; i < answers.Length; i++ )
            {
                answers[i].ShowAnswer();
            }
        }
        public bool IsCorrect(int InputNumber)
        {
            Answer answer = answers[InputNumber - 1];
            if (answer is CorrectAnswer)
            {
             return true;
            }
            return false;
        }
    }
}
