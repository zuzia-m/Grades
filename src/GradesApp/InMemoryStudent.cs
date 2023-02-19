using System;
using System.Collections.Generic;
using System.Text;

namespace GradesApp
{
    public class InMemoryStudent : StudentBase
    {
        private List<double> grades;
        private string firstName;
        private string lastName;

        public override string FirstName
        {
            get
            {
                return $"{char.ToUpper(firstName[0])}{firstName.Substring(1, firstName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
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
                {
                    lastName = value;
                }
            }
        }

        public InMemoryStudent(string firstName, string lastName) : base(firstName, lastName)
        {
            grades = new List<double>();
        }

        public void ChangeStudentName(string newName)
        {
            string oldName = this.FirstName;
            foreach (char c in newName)
            {
                if (char.IsDigit(c))
                {
                    this.FirstName = oldName;
                    break;
                }
                else
                {
                    this.FirstName = newName;
                }
            }
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6)
            {
                grades.Add(grade);
                if (grade < 3)
                {
                    CheckEventGradeUnder3();
                }
            }
            else
            {
                throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
            }
        }

        public override void ShowGrades()
        {
            StringBuilder sb = new StringBuilder($"{this.FirstName} {this.LastName} grades are: ");
            for (int i = 0; i < grades.Count; i++)
            {
                if (i == grades.Count - 1)
                {
                    sb.Append($"{grades[i]}.");
                }
                else
                {
                    sb.Append($"{grades[i]}; ");
                }
            }
            Console.WriteLine($"\n{sb}");
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