using System;
using Xunit;
using GradesApp;

namespace Grades.Tests
{
    public class TypeTests
    {
        [Fact]
        public void GetStudentReturnsDifferentObjects()
        {
            var student1 = GetStudent("Robert", "Lewandowski");
            var student2 = GetStudent("Lionel", "Messi");

            Assert.NotSame(student1, student2);
            Assert.False(student1.Equals(student2));
            Assert.False(Object.ReferenceEquals(student1, student2));
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var student1 = GetStudent("Robert", "Lewandowski");
            var student2 = student1;

            Assert.Same(student1, student2);
            Assert.True(student1.Equals(student2));
            Assert.True(Object.ReferenceEquals(student1, student2));
        }

        private StudentInMemory GetStudent(string firstName, string secondName)
        {
            return new StudentInMemory(firstName, secondName);
        }
    }
}