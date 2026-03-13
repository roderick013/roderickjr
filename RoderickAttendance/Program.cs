using System;
using AttendanceManagementDataService;
using AttendanceManagementAppService;

namespace roderickjr
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Attendance Tracker === ");

            string username = "roderickorfella";
            string password = "roderick";

            Console.Write("Enter Username: ");
            string userInput = Console.ReadLine();
            Console.Write("Enter Password: ");
            string passInput = Console.ReadLine();

            if (userInput != username || passInput != password)
            {
                Console.WriteLine("Invalid username or Password. Access denied.");
                return;
            }

            Console.WriteLine($"Login successful! Welcome, {username}!");

            string[] studentNames = { "Ubaldo", "Alatiit", "Redondo", "Sanlorenzo", "Igloso", "Aviles" };
            int days = 3;

            var dataService = new AttendanceDataService(studentNames, days);
            var appService = new AttendanceAppService(dataService);

            for (int day = 0; day < days; day++)
            {
                Console.WriteLine($"\n--- Day {day + 1} Attendance ---");
                appService.RecordDayAttendance(day);
                appService.PrintDailySummary(day);
            }

            appService.PrintOverallSummary();
        }
    }
}