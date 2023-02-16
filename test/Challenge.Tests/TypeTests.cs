using System;
using ChallengeApp;
using Xunit;

namespace Challenge.Tests
{
    public class TypeTests
    {
        [Fact]
        public void GetEmployeeReturnsDirrefentsObjects()
        {
            var employee1 = GetEmployee("Rudnicki", "Paweł");
            var employee2 = GetEmployee("Kowalski", "Olaf");

            Assert.NotSame(employee1, employee2);
            Assert.False(employee1.Equals(employee2));
            Assert.False(Object.ReferenceEquals(employee1, employee2));
        }
        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var employee1 = GetEmployee("Rudnicki", "Paweł");
            var employee2 = employee1;

            Assert.Same(employee1, employee2);
            Assert.True(employee1.Equals(employee2));
            Assert.True(Object.ReferenceEquals(employee1, employee2));
        }

        private InMemoryEmployee GetEmployee(string lastName, string firstName)
        {
            return new InMemoryEmployee(lastName, firstName);
        }
    }
}