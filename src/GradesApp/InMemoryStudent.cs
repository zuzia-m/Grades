using System;
using System.Collections.Generic;
using System.Text;

namespace GradesApp
{
    public class InMemoryStudent : StudentBase
    {
        private List<double> grades;
        private string firstName;
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
        private string lastName;
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
                    this.FirstName = newName;
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
                throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
        }

        public override void AddGrade(string grade)
        {
            if (grade.Contains('+') || grade.Contains('-'))
            {
                switch (grade)
                {
                    case "1+":
                        this.grades.Add(1.5);
                        CheckEventGradeUnder3();
                        break;

                    case "2-":
                        this.grades.Add(1.75);
                        CheckEventGradeUnder3();
                        break;

                    case "2+":
                        this.grades.Add(2.5);
                        CheckEventGradeUnder3();
                        break;

                    case "3-":
                        this.grades.Add(2.75);
                        CheckEventGradeUnder3();
                        break;

                    case "3+":
                        this.grades.Add(3.5);
                        break;

                    case "4-":
                        this.grades.Add(3.75);
                        break;

                    case "4+":
                        this.grades.Add(4.5);
                        break;

                    case "5-":
                        this.grades.Add(4.75);
                        break;

                    case "5+":
                        this.grades.Add(5.5);
                        break;

                    case "6-":
                        this.grades.Add(5.75);
                        break;

                    default:
                        throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
                }
            }
            else
            {
                double gradeDouble = 0;
                var isParsed = double.TryParse(grade, out gradeDouble);
                if (isParsed && gradeDouble > 0 && gradeDouble <= 6)
                {
                    this.grades.Add(gradeDouble);
                    if (gradeDouble < 3)
                    {
                        CheckEventGradeUnder3();
                    }
                }
                else
                    throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
            }
        }
        public void ShowGrades()
        {
            StringBuilder sb = new StringBuilder($"{this.FirstName} {this.LastName} grades are: ");
            for (int i = 0; i < grades.Count; i++)
            {
                if (i == grades.Count - 1)
                    sb.Append($"{grades[i]:N2}.");
                else
                    sb.Append($"{grades[i]:N2}; ");
            }
            Console.WriteLine(sb);
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