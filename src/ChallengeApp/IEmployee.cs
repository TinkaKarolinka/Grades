using System;

namespace ChallengeApp
{
    public interface IEmployee
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        delegate void GradeAddedDelegate(object sender, EventArgs arg);
        event GradeAddedDelegate GradeAdded;
        event GradeAddedDelegate GradeAddedUnder30;
        void AddGrade(double grade);
        void AddGrade(string grade);
        Statistics GetStatistics();
        void ShowStatistics();
    }
}