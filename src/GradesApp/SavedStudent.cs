using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GradesApp
{
    public class SavedStudent : StudentBase
    {
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
                return $"{char.ToUpper(lastName[0])}{lastName.Substring(1, lastName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    lastName = value;
                }
            }
        }
        public SavedStudent(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        const string fileName = "_grades.txt";

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6)
            {
                using (var writer = File.AppendText($"{FirstName}_{LastName}{fileName}"))
                using (var writer2 = File.AppendText($"audit.txt"))
                {
                    writer.WriteLine(grade);
                    writer2.WriteLine($"{FirstName} {LastName} - {grade}        {DateTime.UtcNow}");
                    if (grade < 3)
                    {
                        CheckEventGradeUnder3();
                    }
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
                        AddGrade(1.5);
                        break;

                    case "2-":
                        AddGrade(1.75);
                        break;

                    case "2+":
                        AddGrade(2.5);
                        break;

                    case "3-":
                        AddGrade(2.75);
                        break;

                    case "3+":
                        AddGrade(3.5);
                        break;

                    case "4-":
                        AddGrade(3.75);
                        break;

                    case "4+":
                        AddGrade(4.5);
                        break;

                    case "5-":
                        AddGrade(4.75);
                        break;

                    case "5+":
                        AddGrade(5.5);
                        break;

                    case "6-":
                        AddGrade(5.5);
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
                    throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
            }
        }
        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{FirstName}_{LastName}{fileName}"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}