using System;
using ChallengeApp;
using Xunit;

namespace Challenge.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void Test1()
        {
            // arrange
            var employee = new InMemoryEmployee("Lubicz", "Ola");
            employee.AddGrade(89.1);
            employee.AddGrade(90.5);
            employee.AddGrade(77.3);

            // act
            var result = employee.GetStatistics();

            // assert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.High);
            Assert.Equal(77.3, result.Low);
        }
    }
}