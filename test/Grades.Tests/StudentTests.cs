using System;
using Xunit;
using GradesApp;

namespace Grades.Tests
{
    public class StudentTests
    {
        [Fact]
        public void Test1()
        {
            // arrange
            var student = new InMemoryStudent("Zuzia", "Macińska");
            student.AddGrade(5.0);
            student.AddGrade(6.0);
            student.AddGrade(4.5);
            student.AddGrade(3.5);
            student.AddGrade(5.0);

            // act
            var result = student.GetStatistics();

            // assert
            Assert.Equal(4.8, result.Average, 1);
            Assert.Equal(6.0, result.High, 1);
            Assert.Equal(3.5, result.Low, 1);
        }
    }
}
