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

        public override void AddGrade(string grade)
        {
            if (grade.Length == 2 && char.IsDigit(grade[0]) && grade[0] <= '6' && (grade[1] == '+' || grade[1] == '-'))
            {
                double convertedGradeToDouble = char.GetNumericValue(grade[0]);
                switch (grade[1])
                {
                    case '+':
                        double gradePlus = convertedGradeToDouble + 0.50;
                        if (gradePlus > 1 && gradePlus <= 6)
                        {
                            this.grades.Add(gradePlus);
                            if (gradePlus < 3)
                            {
                                CheckEventGradeUnder3();
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
                        }
                        break;

                    case '-':
                        double gradeMinus = convertedGradeToDouble - 0.25;
                        if (gradeMinus > 1 && gradeMinus <= 6)
                        {
                            this.grades.Add(gradeMinus);
                            if (gradeMinus < 3)
                            {
                                CheckEventGradeUnder3();
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed! ");
                        }
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
                {
                    throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
                }
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