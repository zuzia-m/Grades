using System;

namespace GradesApp
{
    class Program
    {
        static void OnGradeUnder3(object sender, EventArgs args)
        {
            WritelineColor(ConsoleColor.DarkYellow, $"Oh no! Student got grade under 3. We should inform student’s parents about this fact!");
        }

        static void Main(string[] args)
        {
            WritelineColor(ConsoleColor.Magenta, "Hello to the [Student's Grades Book] console app.");

            bool CloseApp = false;

            while (!CloseApp)
            {
                Console.WriteLine();
                WritelineColor(ConsoleColor.Cyan,
                    "1 - Add student's grades to the program memory and show statistics\n" +
                    "2 - Add student's grades to the .txt file and show statistics\n" +
                    "X - Close app\n");

                WritelineColor(ConsoleColor.Yellow, "What you want to do? \nPress key 1, 2 or X: ");
                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case "1":
                        string firstName, lastName;
                        InsertFirstNameAndLastName(out firstName, out lastName);
                        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(firstName))
                        {
                            var student = new InMemoryStudent(firstName, lastName);
                            student.GradeUnder3 += OnGradeUnder3;
                            EnterGrade(student);
                            student.ShowStatistics();
                        }
                        else
                            WritelineColor(ConsoleColor.Red, "Student's firstname and lastname can not be empty!");
                        break;
                    case "2":
                        string firstName2, lastName2;
                        InsertFirstNameAndLastName(out firstName2, out lastName2);
                        if (!string.IsNullOrEmpty(firstName2) && !string.IsNullOrEmpty(firstName2))
                        {
                            var student2 = new SavedStudent(firstName2, lastName2);
                            student2.GradeUnder3 += OnGradeUnder3;
                            EnterGrade(student2);
                            student2.ShowStatistics();
                        }
                        else
                            WritelineColor(ConsoleColor.Red, "Student's firstname and lastname can not be empty!");
                        break;
                    case "X":
                        CloseApp = true;
                        break;
                    default:
                        WritelineColor(ConsoleColor.Red, "Invalid operation.\n");
                        continue;
                }
            }

            WritelineColor(ConsoleColor.DarkYellow, "\n\nBye Bye! Press any key to leave.");
            Console.ReadKey();
        }

        private static void InsertFirstNameAndLastName(out string firstName, out string lastName)
        {
            WritelineColor(ConsoleColor.Yellow, "Please insert student's first name: ");
            firstName = Console.ReadLine();
            WritelineColor(ConsoleColor.Yellow, "Please insert students last name: ");
            lastName = Console.ReadLine();
        }

        static void EnterGrade(IStudent student)
        {
            while (true)
            {
                WritelineColor(ConsoleColor.Yellow, $"Enter grade for {student.FirstName} {student.LastName}:");
                var input = Console.ReadLine();

                if (input == "q" || input == "Q")
                    break;
                try
                {
                    student.AddGrade(input);
                }
                catch (FormatException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                catch (ArgumentException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                finally
                {
                    WritelineColor(ConsoleColor.DarkMagenta, $"To leave and show {student.FirstName} {student.LastName} statistics enter 'q'.");
                }
            }
        }
        static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}


