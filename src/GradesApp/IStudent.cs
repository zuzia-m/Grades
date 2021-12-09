using System;
using System.Collections.Generic;
using System.Text;

namespace GradesApp
{
    public interface IStudent
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        event GradeAddedUnder3Delegade GradeUnder3;

        void AddGrade(double grade);
        void AddGrade(string grade);
        void ShowGrades();
        Statistics GetStatistics();
        void ShowStatistics();
    }
}