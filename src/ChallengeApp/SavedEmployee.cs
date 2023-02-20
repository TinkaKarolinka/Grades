using System;
using System.Linq;
using System.IO;

namespace ChallengeApp
{
    internal class SavedEmployee : EmployeeBase
    {
        private const string auditFileName = "_audit.txt";
        private string fullFileName;
        private string firstName;
        private string lastName;
        public override event IEmployee.GradeAddedDelegate GradeAdded;
        public override event IEmployee.GradeAddedDelegate GradeAddedUnder30;
        public SavedEmployee(string lastName, string firstName) : base(lastName, firstName)
        {
            fullFileName = $"{lastName}_{firstName}{auditFileName}";
        }

        public override string FirstName
        {
            get
            {
                return $"{char.ToUpper(firstName[0])}{firstName.Substring(1, firstName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    if (!value.All(char.IsDigit))
                    {
                        firstName = value;
                    }
            }
        }

        public override string LastName
        {
            get
            {
                return $"{char.ToUpper(lastName[0])}{lastName.Substring(1, lastName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    if (!value.All(char.IsDigit))
                    {
                        lastName = value;
                    }
            }
        }
        
        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 100)
            {
                using (var writer = File.AppendText($"{fullFileName}"))
                {
                    writer.WriteLine($"{grade:N2}");
                }
                using (var writerAudit = File.AppendText($"_audit.txt"))
                {
                    writerAudit.WriteLine($"Ocena: {grade:N2} | Data: {DateTime.Now}");
                }
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
                if (GradeAddedUnder30 != null && grade < 30)
                {
                    GradeAddedUnder30(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"{grade} nie jest poprawną wartością. Ocena musi zawierać się w przedziale 0,5-100.");
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            if (File.Exists($"{fullFileName}"))
            {
                using (var streamReaderFromEmpGrades = File.OpenText($"{fullFileName}"))
                {
                    var line = streamReaderFromEmpGrades.ReadLine();

                    while (line != null)
                    {
                        bool CanParse;
                        double parseResult;
                        if (CanParse = double.TryParse(line, out parseResult))
                        {
                            result.Add(parseResult);
                        }
                        line = streamReaderFromEmpGrades.ReadLine();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Plik o tej nazwie nie istnieje.");
            }
            return result;
        }
    }
}
