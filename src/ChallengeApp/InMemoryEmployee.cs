using System;
using System.Linq;
using System.Collections.Generic;

namespace ChallengeApp
{
    public class InMemoryEmployee : EmployeeBase
    {
        private List<double> grades;
        private string firstName;
        private string lastName;
        public override event IEmployee.GradeAddedDelegate GradeAdded;
        public override event IEmployee.GradeAddedDelegate GradeAddedUnder30;
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
                return $"{char.ToUpper(lastName[0])}.";
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
        public InMemoryEmployee(string lastName, string firstName) : base(lastName, firstName)
        {
            grades = new List<double>();
        }
        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 100)
            {
                grades.Add(grade);
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
                throw new ArgumentException($"Nieprawidłowy argument: {nameof(grade)}. Ocena musi zawierać się w przedziale 0,5-100!");
            }
        }
        public override void AddGrade(string grade)
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
                    grades.Add(gradeSing);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                    if (GradeAddedUnder30 != null && gradeSing < 30)
                    {
                        GradeAddedUnder30(this, new EventArgs());
                    }
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
                    grades.Add(gradeDouble);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                    if (GradeAddedUnder30 != null && gradeDouble < 30)
                    {
                        GradeAddedUnder30(this, new EventArgs());
                    }
                }
                else
                {
                    throw new ArgumentException($"Nieprawidłowy argument: {nameof(grade)}. Ocena musi zawierać się w przedziale 0,5-100!");
                }
            }
        }
        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            foreach (var grade in grades)
            {
                result.Add(grade);
            }
            return result;
        }
    }
}



