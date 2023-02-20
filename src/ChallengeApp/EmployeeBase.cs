using System;
using System.Collections.Generic;

namespace ChallengeApp
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public delegate void GradeAddedUnder30Delegate(object sender, EventArgs args);
    public abstract class EmployeeBase : NamedObject, IEmployee
    {
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public EmployeeBase(string lastName, string firstName) : base(lastName, firstName)
        {

        }
        public abstract event IEmployee.GradeAddedDelegate GradeAdded;
        public abstract event IEmployee.GradeAddedDelegate GradeAddedUnder30;
        public abstract void AddGrade(double grade);
        public void AddGrade(string grade)
        {
            if ((grade[^1] == '+' || grade[^1] == '-'))
            {
                var modifier = grade[^1];
                var gradeSing = double.Parse(grade[..^1]) + modifier
                switch
                {
                    '+' => .5,
                    '-' => -.25,
                    _ => 0
                };
                if (gradeSing > 0 && gradeSing <= 100)
                {
                    AddGrade(gradeSing);
                }
                else
                {
                    throw new ArgumentException($"Nieprawidłowy argument: {nameof(grade)}. Ocena musi zawierać się w przedziale 0,5-100!");
                }
            }
            else
            {
                double gradeDouble = 0;
                var isParsed = double.TryParse(grade, out gradeDouble);
                if (isParsed && gradeDouble > 0 && gradeDouble <= 100)
                {
                    AddGrade(gradeDouble);
                }
                else
                {
                    throw new ArgumentException($"Nieprawidłowy argument: {nameof(grade)}. Ocena musi zawierać się w przedziale 0,5-100!");
                }
            }
        }

        public abstract Statistics GetStatistics();
        public void ShowStatistics()
        {
            var stat = GetStatistics();
            if (stat.Count != 0)
            {
                WritelineColor(ConsoleColor.Yellow, "--------------------");
                WritelineColor(ConsoleColor.Yellow, $"{FirstName} {LastName} statystyki:");
                WritelineColor(ConsoleColor.Yellow, $"Ilość ocen: {stat.Count}");
                WritelineColor(ConsoleColor.Yellow, $"Najwyższa ocena: {stat.High:N2}");
                WritelineColor(ConsoleColor.Yellow, $"Najniższa ocena: {stat.Low:N2}");
                WritelineColor(ConsoleColor.Yellow, $"Średnia: {stat.Average:N2}");
                WritelineColor(ConsoleColor.Yellow, "--------------------");
                WritelineColor(ConsoleColor.Blue, "Do zobaczenia ponownie!");
            }
            else
            {
                WritelineColor(ConsoleColor.Red, $"Nie można uzyskać statystyk dotyczących {this.FirstName} {this.LastName}, ponieważ nie dodano żadnej oceny.");
            }
        }

        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
