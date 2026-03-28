using System;
using System.Collections.Generic;
using AttendanceManagementDataService;
using AttendanceManagementAppService;
using AttendanceManagementModels;

namespace roderickjr
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Attendance Tracker ===");

          
            while (true)
            {
                Console.Write("\nEnter Username: ");
                string u = Console.ReadLine() ?? "";
                Console.Write("Enter Password: ");
                string p = Console.ReadLine() ?? "";

                if (u == "roderickorfella" && p == "roderick")
                {
                    Console.WriteLine($"\nLogin successful! Welcome, {u}!");
                    break;
                }
                Console.WriteLine("Invalid credentials. Try again.");
            }

            var dataService = new AttendanceDataService();
            var jsonService = new JsonDataService();
            int totalDays = 3;
            var appService = new AttendanceAppService(dataService, totalDays);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. View/Add Students");
                Console.WriteLine("2. Update Student Name");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Record New Attendance (Bulk)");
                Console.WriteLine("5. Edit Specific Attendance Record"); 
                Console.WriteLine("6. View Overall Summary (Sync to JSON)");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        var students = dataService.GetStudents(totalDays);
                        Console.WriteLine("\n--- Current Students ---");
                        foreach (var s in students) Console.WriteLine($"- {s.Name}");
                        Console.Write("\nEnter new name (or Enter to skip): ");
                        string name = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(name)) dataService.AddStudent(name);
                        break;

                    case "2":
                        Console.Write("Enter current name: ");
                        string oldN = Console.ReadLine() ?? "";
                        Console.Write("Enter new name: ");
                        string newN = Console.ReadLine() ?? "";
                        dataService.UpdateStudent(oldN, newN);
                        break;

                    case "3":
                        Console.Write("Enter name to delete: ");
                        dataService.DeleteStudent(Console.ReadLine() ?? "");
                        break;

                    case "4":
                        for (int day = 0; day < totalDays; day++)
                        {
                            Console.WriteLine($"\n--- Day {day + 1} ---");
                            appService.RecordDayAttendance(day);
                        }
                        break;

                    case "5": 
                        Console.Write("Enter Student Name: ");
                        string targetName = Console.ReadLine() ?? "";
                        Console.Write($"Enter Day Index (0 to {totalDays - 1}): ");
                        if (int.TryParse(Console.ReadLine(), out int targetDay))
                        {
                            Console.Write("Enter New Status (P for Present / A for Absent): ");
                            string input = Console.ReadLine()?.ToLower() ?? "";
                            int newStatus = (input == "p") ? 1 : 0;

                           
                            dataService.RecordAttendance(targetName, targetDay, newStatus);

                    
                            jsonService.SaveToJson(dataService.GetStudents(totalDays));
                            Console.WriteLine("Record updated successfully!");
                        }
                        break;

                    case "6":
                        appService.PrintOverallSummary();
                        jsonService.SaveToJson(dataService.GetStudents(totalDays));
                        break;

                    case "7":
                        jsonService.SaveToJson(dataService.GetStudents(totalDays));
                        running = false;
                        break;
                }
            }
        }
    }
}