using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace qz
{
    internal class Program
    {
        public class Question
        {
            public int QuestionNumber { get; set; }
            public string Description { get; set; }
            public string Answer { get; set; }
        }
        public class Quiz
        {
            public void AddQuestion(Question q)
            {
                q.QuestionNumber = Questions.Count;
                q.Description += " (" + (q.QuestionNumber + 1) + ")".ToString();
                Questions.Add(q);

                if (Questions.Count > QuizLength)
                {
                    Console.WriteLine("Необходимо изменить размер вопроса.");
                }
            }
            public void ReadQuestions(int customIndex = 0)
            {
                if (customIndex == QuizLength)
                {
                    Console.WriteLine(Environment.NewLine + "Викториан закончена.");
                    for (int x = 0; x < Questions.Count; x++)
                        Console.WriteLine($"Вопрос {x + 1} - {Questions[x].Description} | Ответ - {Questions[x].Answer}");
                    Console.WriteLine($"Оценка {Score}/{QuizLength}");
                }
                else
                    while (customIndex < Questions.Count)
                    {
                        Timer timer = new Timer(QuestionTimeLimit);
                        timer.Start();
                        timer.Elapsed += (sender, e) =>
                        {
                            Console.WriteLine("Время завершено...");
                            timer.Stop();
                            ReadQuestions(customIndex + 1);
                        };

                        Console.WriteLine(Questions[customIndex].Description);
                        string input = Console.ReadLine();
                        if (input.ToLower().Trim() == Questions[customIndex].Answer.ToLower().Trim())
                        {
                            Console.WriteLine("Правильный ответ");
                            timer.Stop();
                            customIndex++;
                            Score++;
                        }
                        else
                        {
                            Console.WriteLine("Не верно подумай ещё");
                            break;
                        }
                    }
            }
            public List<Question> Questions = new List<Question>();
            public int Score { get; set; }
            public int QuizLength { get; set; }
            public bool HasTimeLimit { get; set; }

            private int _QuestionTimeLimit;
            public int QuestionTimeLimit
            {
                get
                {
                    return _QuestionTimeLimit;
                }
                set
                {
                    if (HasTimeLimit == true)
                        _QuestionTimeLimit = value;
                    else
                        Console.WriteLine("Вы не назначили значение ограничения по времени.");
                }
            }
            static class Program
            {
                static void Main(string[] args)
                {
                    Quiz q = new Quiz()
                    {
                        HasTimeLimit = true,
                        QuizLength = 4,
                        QuestionTimeLimit = 30000,
                    };
                    q.AddQuestion(new Question() { Description = "Дата ледового побоища", Answer = "1242", });
                    q.AddQuestion(new Question() { Description = "Дата крещения руси", Answer = "988", });
                    q.AddQuestion(new Question() { Description = "Кто написал Шерлока Холмса?", Answer = "Артур Конан Дойл", });
                    q.AddQuestion(new Question() { Description = "Столица Англии", Answer = "Лондон", });
                    q.ReadQuestions();

                    Console.ReadLine();
                }
            }

        }
    }
}
