using System;

namespace GradesApp
{
    internal class Program
    {
        private static void Main(string[] args)
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
                        AddGradesToMemory();
                        break;

                    case "2":
                        AddGradesToTxtFile();
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

        static void OnGradeUnder3(object sender, EventArgs args)
        {
            WritelineColor(ConsoleColor.DarkYellow, $"Oh no! Student got grade under 3. We should inform student’s parents about this fact!");
        }

        private static void AddGradesToMemory()
        {
            string firstName = GetValueFromUser("Please insert student's first name: ");
            string lastName = GetValueFromUser("Please insert student's last name: ");
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(firstName))
            {
                var student = new InMemoryStudent(firstName, lastName);
                student.GradeUnder3 += OnGradeUnder3;
                EnterGrade(student);
                student.ShowStatistics();
            }
            else
            {
                WritelineColor(ConsoleColor.Red, "Student's firstname and lastname can not be empty!");
            }
        }

        private static void AddGradesToTxtFile()
        {
            string firstName = GetValueFromUser("Please insert student's first name: ");
            string lastName = GetValueFromUser("Please insert student's last name: ");
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(firstName))
            {
                var student2 = new SavedStudent(firstName, lastName);
                student2.GradeUnder3 += OnGradeUnder3;
                EnterGrade(student2);
                student2.ShowStatistics();
            }
            else
            {
                WritelineColor(ConsoleColor.Red, "Student's firstname and lastname can not be empty!");
            }
        }

        private static void EnterGrade(IStudent student)
        {
            while (true)
            {
                WritelineColor(ConsoleColor.Yellow, $"Enter grade for {student.FirstName} {student.LastName}:");
                var input = Console.ReadLine();

                if (input == "q" || input == "Q")
                {
                    break;
                }
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

        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static string GetValueFromUser(string comment)
        {
            WritelineColor(ConsoleColor.Yellow, comment);
            string userInput = Console.ReadLine();
            return userInput;
        }
    }
}