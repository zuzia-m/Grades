using System;
using System.Collections.Generic;
using System.Text;

namespace GradesApp
{
public interface IStudent
    {
        void AddGrade(double grade);
        void AddGrade(string grade);
        event GradeAddedUnder3Delegade GradeUnder3;
        string FirstName { get; set; }
        string LastName { get; set; }
        Statistics GetStatistics();
        void ShowStatistics();
    }
}