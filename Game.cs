using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Millionair
{
    class Game
    {
        private User user;
        Collection<Question> questions;
        public void Start()
        {
            Console.WriteLine("Добро пожаловать в игру \"Кто хочет стать миллионером\"!");
            Console.WriteLine("Введите +, если хотите начать новую игру.");
            Console.WriteLine("Введите -, если хотите загрузить сохраненную игру.");
            
          string inputSign =   UserInputSign();
            if (inputSign == "-")
            {
                Load();
            }
            if (inputSign == "+")
            {
               NewGame();
            }
            if (inputSign == "*")
            {
                Console.WriteLine("Введите, пожалуйста, либо +, либо -");
            }
            InitQuestions();
            AskQuestions();
            Finish();
        }

        private void NewGame()
        {
            PrintGameRules();
            IntroduceUser();
            Console.WriteLine($"Текущий счет: {user.Score.TotalScore} BYN");
        }
        private void PrintGameRules()
        {
            Console.WriteLine(ReadTextFromFile("GameRules.txt"));
        }
        private string GetDirectoryName()
        {
            string runningExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            FileInfo runningExecutableFileInfo = new FileInfo(runningExecutable);
            return runningExecutableFileInfo.DirectoryName;
        }
        private string ReadTextFromFile(string fileName)
        {

            return File.ReadAllText(Path.Combine(GetDirectoryName(), fileName));
        }
        private void IntroduceUser()
        {
            Console.WriteLine("Введите свое имя: ");
            this.user = new User(Console.ReadLine(), 0);
        }
        private void InitQuestions()
        {
            string allQuestionText = ReadTextFromFile("Questions.txt");
            string[] lines = allQuestionText.Split(Environment.NewLine);
            this.questions = new Collection<Question>();
            Collection<Answer> answers = new Collection<Answer>();
           
            string currentQuestion = string.Empty;
            foreach (string line in lines)
            {

                if (string.Equals(line, string.Empty))
                {
                    questions.Add(new Question(currentQuestion, answers.ToArray()));
                    
                    continue;
                }
                if (line.StartsWith("+") || line.StartsWith("-"))
                {
                    if (line.StartsWith("+"))
                    {
                        answers.Add(new CorrectAnswer(line));
                    }
                    if (line.StartsWith("-"))
                    {
                        answers.Add(new WrongAnswer(line));
                    }
                }
                else
                {
                    currentQuestion = line;
                    answers.Clear();
                }
            }
        }
        private int QuestionToSkip()
        {
            int skipQuestion = 0;
            Score counter = new Score();
            if (user.Score.TotalScore > 0)
            {
                do
                {
                    skipQuestion++;
                    counter.Increase();
                }
                while (counter.TotalScore < user.Score.TotalScore);
            }
            return skipQuestion;
        }
        private void AskQuestions()
        {
            int questionToSkip = QuestionToSkip();
            int i = questionToSkip;
            foreach (Question question in questions)
            {
                if(questionToSkip > 0)
                {
                    questionToSkip--;
                    continue;
                }
                
               
                question.ShowQuestion();
                int inputNumber = user.UserInputNumber("Введите номер правильного ответа:");
                if (question.IsCorrect(inputNumber))
                {
                    user.Score.Increase();
                    if (i == questions.Count - 1)
                    {
                        Console.WriteLine("Поздравляем! Вы ответили на все вопросы и выиграли главный приз: " + user.Score.TotalScore + "BYN!");
                    }
                    else
                    {
                        Console.WriteLine("Верно! Ваш выигрыш: " + user.Score.TotalScore + "BYN");
                        i++;
                        if (AskAboutContinue() is false)
                        {
                            break;
                        }
                    }
                    

                }
                else
                {
                    user.Score.Reset();
                    Console.WriteLine("К сожалению, ответ неверный. Ваш выигрыш: " + user.Score.TotalScore + "BYN");
                    break;
                }
            }

           
        }
        private bool AskAboutContinue()
        {
            Console.WriteLine("");
            Console.WriteLine("Вы  можете забрать деньги сейчас, продолжить игру или сохранить игру.");
            Console.WriteLine("Введите +, если хотите продолжить игру.");
            Console.WriteLine("Введите -, если хотите забрать выигрыш и завершить игру.");
            Console.WriteLine("Введите *, если хотите сохранить игру.");
            string inputSign = UserInputSign();
            if (inputSign == "+")
            {

            }
            if (inputSign == "-")
            {
                Console.WriteLine("Ваш выигрыш: " + user.Score.TotalScore + " BYN");
                return false;
            }
            if (inputSign == "*")
            {
                Save();
                return false;
            }
            return true;
        }
        private void Finish()
        {
            Console.WriteLine("Спасибо за участие!");
            Console.WriteLine("Игра завершена.");
        }
        private void Save()
        {
            Console.WriteLine("Введите имя для сохранения:");
            do
            {
                string name = Console.ReadLine();
                string fullpath = Path.Combine(GetDirectoryName(), name + ".sav");
                try
                {
                    File.WriteAllText(fullpath, $"{user.Name}{ Environment.NewLine}{ user.Score.TotalScore}");
                    Console.WriteLine("Файл сохранен");
                    return;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"К сожалению, не удалось сохранить файл:  {fullpath}. Попробуйте ввести другое имя.");
                }
            }
            while (true);

        }
        private void Load()
        {
            DirectoryInfo directory = new DirectoryInfo(GetDirectoryName());
            FileInfo[] saves = directory.GetFiles("*.sav");
            for (int i = 0; i < saves.Length; i++)
            {
                Console.WriteLine((i + 1) + ")" + saves[i].Name);
            }
            int gameNumber = ChooseLoadGame(saves);
            FileInfo file = saves[gameNumber - 1];
            string allText = File.ReadAllText(file.FullName);
            string[] parts = allText.Split(Environment.NewLine);
            user = new User(parts[0], int.Parse(parts[1]));
            
            }
        private int ChooseLoadGame(FileInfo[] fileinfo)
            {
            Console.WriteLine("Введите порядковый номер игры,которую хотите открыть:");
            string number = Console.ReadLine();
            try
            {
                int gameNumber = int.Parse(number);
                if (gameNumber <= (fileinfo.Length) && gameNumber > 0)
                {
                    return gameNumber;
                }
                else
                {
                    Console.WriteLine($"Можно вводить только целые числа от 1 до {fileinfo.Length}");
                    return ChooseLoadGame(fileinfo);
                }
            }

            catch (FormatException)
            {
                Console.WriteLine($"Можно вводить только целые числа от 1 до {fileinfo.Length}");
                return ChooseLoadGame(fileinfo);
                
            }
        }
        private string UserInputSign()
        {
            string InputSign = Console.ReadLine();
            if (InputSign == "+" || InputSign == "-" || InputSign == "*")
            {
                return InputSign;
            }
            else
            {
                Console.WriteLine("Можно ввести либо +, либо -, либо * ");
                return UserInputSign();
            }
        }
    }
}

