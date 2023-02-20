using System;

namespace GradesApp
{
    public abstract class StudentBase : Person, IStudent
    {
        public delegate void GradeAddedUnder3Delegade(object sender, EventArgs args);
        public event GradeAddedUnder3Delegade GradeUnder3;
        public override string FirstName { get; set; }
        public override string LastName { get; set; }

        public StudentBase(string firstName, string lastName) : base(firstName, lastName)
        {
        }


        public abstract void AddGrade(double grade);

        public void AddGrade(string grade)
        {
            double convertedGradeToDouble = char.GetNumericValue(grade[0]);
            if (grade.Length == 2 && char.IsDigit(grade[0]) && grade[0] <= '6' && (grade[1] == '+' || grade[1] == '-'))
            {
                switch (grade[1])
                {
                    case '+':
                        double gradePlus = convertedGradeToDouble + 0.50;
                        if (gradePlus > 1 && gradePlus <= 6)
                        {
                            AddGrade(gradePlus);
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
                        }
                        break;

                    case '-':
                        double gradeMinus = convertedGradeToDouble - 0.250;
                        if (gradeMinus > 1 && gradeMinus <= 6)
                        {
                            AddGrade(gradeMinus);
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
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
                    AddGrade(gradeDouble);
                }
                else
                {
                    throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
                }
            }
        }

        public abstract void ShowGrades();

        public abstract Statistics GetStatistics();

        public void ShowStatistics()
        {
            var stat = GetStatistics();
            if (stat.Count != 0)
            {
                ShowGrades();
                Console.WriteLine($"{FirstName} {LastName} statistics:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Total grades: {stat.Count}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Highest grade: {stat.High:N2}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Lowest grade: {stat.Low:N2}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Average: {stat.Average:N2}");
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