using System;
using System.Linq;

namespace ChallengeApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WritelineColor(ConsoleColor.Yellow, "--------------------\n");
            WritelineColor(ConsoleColor.Yellow, "Witaj! Dzięki tej aplikacji możesz kontrolować poziom wydajności swoich pracowników.\n");
            WritelineColor(ConsoleColor.Yellow, "Dodawaj oceny i poznawaj statystyki.\n");
            WritelineColor(ConsoleColor.Yellow, "Aby zacząć wybierz jedną z opcji:\n");
            WritelineColor(ConsoleColor.Yellow, "--------------------\n");
            WritelineColor(ConsoleColor.Yellow, "MENU: \n");
            WritelineColor(ConsoleColor.Yellow, "'1'. Utwórz nowego pracownika i dodaj dla niego oceny - w pamięci\n");
            WritelineColor(ConsoleColor.Yellow, "'2'. Utwórz nowego pracownika i dodaj dla niego oceny - w pliku\n");
            WritelineColor(ConsoleColor.Yellow, "'Q'. Wyjście\n");
            WritelineColor(ConsoleColor.Yellow, "--------------------\n");

            var input = Console.ReadLine().ToUpper().Trim();

            while (true)
            {
                if (input == "Q")
                {
                    break;
                }
                else if (input == "1")
                {
                    AddEmployeeAndGradesInMemory();
                    break;
                }
                else if (input == "2")
                {
                    AddEmployeeAndGradesInFile();
                    break;
                }
                else
                {
                    WritelineColor(ConsoleColor.Red, $"Niepoprawny wybór. Wciśnij 1, 2 lub Q.\n");
                    return;
                }
            }
        }
        private static void AddEmployeeAndGradesInMemory()
        {
            while (true)
            {
                try
                {
                    WritelineColor(ConsoleColor.Yellow, $"Proszę wpisać nazwisko pracownika, którego chcesz ocenić. Nazwisko nie może składać się wyłącznie z cyfr!\n");
                    var lastName = Console.ReadLine();
                    WritelineColor(ConsoleColor.Yellow, $"Proszę wpisać imię pracownika, którego chcesz ocenić. Imię nie może składać się wyłącznie z cyfr!\n");
                    var firstName = Console.ReadLine();
                    if (!firstName.All(char.IsDigit) && !lastName.All(char.IsDigit))
                        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                        {
                            var inMemoryEmployee = new InMemoryEmployee(lastName, firstName);
                            inMemoryEmployee.GradeAdded += OnGradeAdded;
                            inMemoryEmployee.GradeAddedUnder30 += OnGradeUnder30;
                            EnterGrade(inMemoryEmployee);
                            inMemoryEmployee.ShowStatistics();
                            break;
                        }
                }
                catch
                {
                    WritelineColor(ConsoleColor.Red, "Nazwisko i imię pracownika stanowią puste pola lub zawierają niepoprawne znaki.");
                }
            }
        }
        public static void AddEmployeeAndGradesInFile()
        {
            while (true)
            {
                try
                {
                    WritelineColor(ConsoleColor.Yellow, $"Proszę wpisać nazwisko pracownika, którego chcesz ocenić. Nazwisko nie może składać się wyłącznie z cyfr!\n");
                    var lastName = Console.ReadLine();
                    WritelineColor(ConsoleColor.Yellow, $"Proszę wpisać imię pracownika, którego chcesz ocenić. Imię nie może składać się wyłącznie z cyfr!\n");
                    var firstName = Console.ReadLine();
                    if (!firstName.All(char.IsDigit) && !lastName.All(char.IsDigit))
                        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                        {
                            var savedEmployee = new SavedEmployee(firstName, lastName);
                            savedEmployee.GradeAdded += OnGradeAdded;
                            savedEmployee.GradeAddedUnder30 += OnGradeUnder30;
                            EnterGrade(savedEmployee);
                            savedEmployee.ShowStatistics();
                            break;
                        }
                }
                catch
                {
                    WritelineColor(ConsoleColor.Red, "Nazwisko i imię pracownika stanowią puste pola lub zawierają niepoprawne znaki.");
                }
            }
        }
        private static void EnterGrade(IEmployee employee)
        {
            while (true)
            {
                WritelineColor(ConsoleColor.Blue, $"Wpisz ocenę w przedziale 0,5-100 dla pracownika {employee.LastName} {employee.FirstName}.\n");
                WritelineColor(ConsoleColor.Blue, "Jeśli chcesz otrzymać ocenę np. 10,5 dodaj do pełnej liczby całkowitej '+', jeśli chcesz otrzymać ocenę np. 10,75 to za liczbą napisz znak '-'.\n");

                var input = Console.ReadLine();

                if (input == "q" || input == "Q")
                {
                    break;
                }
                try
                {
                    employee.AddGrade(input);
                }
                catch (FormatException)
                {
                    WritelineColor(ConsoleColor.Red, $"Ocena nie jest liczbą lub zawiera za dużo znaków, niepoprawny format.\n");
                }
                catch (ArgumentException)
                {
                    WritelineColor(ConsoleColor.Red, $"Wpisana wartość nie jest liczbą lub nie zawiera się w przedziale 0,5-100!\n");
                }
                catch (NullReferenceException)
                {
                    WritelineColor(ConsoleColor.Red, $"Ocena musi mieć wartość, nie może być pusta.\n");
                }
                catch (IndexOutOfRangeException)
                {
                    WritelineColor(ConsoleColor.Red, $"Ocena musi zawierać się w przedziale 0,5-100!\n");
                }
                finally
                {
                    WritelineColor(ConsoleColor.Blue, "--------------------\n");
                    WritelineColor(ConsoleColor.Blue, $"Aby wyjść i wyświetlić statystyki dla osoby {employee.LastName} {employee.FirstName} wciśnij 'Q'.\n");
                }
            }
        }
        static void OnGradeUnder30(object sender, EventArgs args)
        {
            WritelineColor(ConsoleColor.Magenta, "Oh nie! Pracownik otrzymał ocenę poniżej 30. Powinniśmy pomyśleć o dodatkowym szkoleniu dla tej osoby.\n");
        }
        static void OnGradeAdded(object sender, EventArgs args)
        {
            WritelineColor(ConsoleColor.Green, "Nowa ocena została dodana.\n");
        }
        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}


