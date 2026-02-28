namespace roderickjr
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Attendance Tracker === ");
            string username = "roderickorfella";
            string password = "roderick";

            Console.WriteLine("Enter Username: ");
            string usernameInput = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string passwordInput = Console.ReadLine();

            if (usernameInput == username && passwordInput == password)
            {
                Console.WriteLine("Login successful! Welcomeme, " + username + "!");

                string[] students = { "Ubaldo", "Alatiit", "Redondo", "Sanlorenzo", "Igloso", "Aviles" };

                int days = 3;
                int[,] attendance = new int[students.Length, days];

                int overallPresent = 0;
                int overallAbsent = 0;

                for (int day = 0; day < days; day++)
                {
                    Console.WriteLine("\n--- Day " + (day + 1) + " Attendance ---");

                    int dailyPresent = 0;
                    int dailyAbsent = 0;

                    for (int i = 0; i < students.Length; i++)
                    {
                        Console.Write("Is " + students[i] + " present? (P/A): ");
                        string status = Console.ReadLine().ToUpper();

                        if (status == "P")
                        {
                            attendance[i, day] = 1;
                            dailyPresent++;
                        }
                        else
                        {
                            attendance[i, day] = 0;
                            dailyAbsent++;
                        }
                    }

                    Console.WriteLine("Day " + (day + 1) + " Present: " + dailyPresent);
                    Console.WriteLine("Day " + (day + 1) + " Absent: " + dailyAbsent);

                    overallPresent += dailyPresent;
                    overallAbsent += dailyAbsent;
                }

                Console.WriteLine("\n=== Each Student Total Attendance ===");

                for (int i = 0; i < students.Length; i++)
                {
                    int studentTotal = 0;

                    for (int day = 0; day < days; day++)
                    {
                        studentTotal += attendance[i, day];
                    }

                    Console.WriteLine(students[i] + " was present for " + studentTotal + " out of " + days + " days.");
                }

            }

            else
            {
                Console.WriteLine("Invalid username or Password. Access denied. ");
                return;
            }
        }
    }
}