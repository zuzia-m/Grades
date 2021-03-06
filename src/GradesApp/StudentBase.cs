using System;
using System.Collections.Generic;
using System.Text;

namespace GradesApp
{

    public delegate void GradeAddedUnder3Delegade(object sender, EventArgs args);

    public abstract class StudentBase : Person, IStudent
    {
        public override string FirstName { get; set; }
        public override string LastName { get; set; }

        public StudentBase(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public event GradeAddedUnder3Delegade GradeUnder3;

        public abstract void AddGrade(double grade);
        public abstract void AddGrade(string grade);
        public abstract void ShowGrades();
        public abstract Statistics GetStatistics();

        public void ShowStatistics()
        {
            if (GetStatistics().Count != 0)
            {
                ShowGrades();
                Console.WriteLine($"{FirstName} {LastName} statistics:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Total grades: {GetStatistics().Count}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Highest grade: {GetStatistics().High:N2}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Lowest grade: {GetStatistics().Low:N2}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Average: {GetStatistics().Average:N2}");
                Console.WriteLine();
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Couldn't get statistics for {this.FirstName} {this.LastName} because no grade has been added.");
                Console.ResetColor();
            }
        }

        protected void CheckEventGradeUnder3()
        {
            if (GradeUnder3 != null)
            {
                GradeUnder3(this, new EventArgs());
            }
        }
    }
}