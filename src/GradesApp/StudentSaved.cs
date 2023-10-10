using System;
using System.IO;
using System.Text;

namespace GradesApp
{
    public class StudentSaved : StudentBase
    {
        private const string fileName = "_grades.txt";

        private string firstName;
        private string lastName;
        private string fullFileName;

        public override string FirstName
        {
            get
            {
                return $"{char.ToUpper(firstName[0])}{firstName.Substring(1, firstName.Length - 1).ToLower()}";
            }
            set
            {
                firstName = value;
            }
        }

        public override string LastName
        {
            get
            {
                return $"{char.ToUpper(lastName[0])}{lastName.Substring(1, lastName.Length - 1).ToLower()}";
            }
            set
            {
                lastName = value;
            }
        }

        public StudentSaved(string firstName, string lastName) : base(firstName, lastName)
        {
            fullFileName = $"{firstName}_{lastName}{fileName}";
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6)
            {
                using (var writer = File.AppendText($"{fullFileName}"))
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
            {
                throw new ArgumentException($"Invalid argument: {nameof(grade)}. Only grades from 1 to 6 are allowed!");
            }
        }

        public override void ShowGrades()
        {
            StringBuilder sb = new StringBuilder($"{this.FirstName} {this.LastName} grades are: ");

            using (var reader = File.OpenText(($"{fullFileName}")))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    sb.Append($"{line}; ");
                    line = reader.ReadLine();
                }
            }
            Console.WriteLine($"\n{sb}");
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            if (File.Exists($"{fullFileName}"))
            {
                using (var reader = File.OpenText($"{fullFileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = double.Parse(line);
                        result.Add(number);
                        line = reader.ReadLine();
                    }
                }
            }
            return result;
        }
    }
}