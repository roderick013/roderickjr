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

                if (u == "admin" && p == "123")
                {
                    Console.WriteLine($"\nLogin successful! Welcome, {u}!");
                    break;
                }
                Console.WriteLine("Invalid credentials. Try again.");
            }

            AttendanceDataService dataService = new AttendanceDataService(new MySqlDataService());
            int totalDays = 3;
            var appService = new AttendanceAppService(totalDays);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. View Students");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Update Student Name");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Record New Attendance (Specific Day)");
                Console.WriteLine("6. Edit Specific Attendance Record"); 
                Console.WriteLine("7. View Overall Summary");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        var students = dataService.GetStudents(totalDays);
                        Console.WriteLine("\n--- Current Students ---");
                        if (students.Count == 0) Console.WriteLine("(No students found)");
                        foreach (var s in students) Console.WriteLine($"- {s.Name}");
                        break;

                    case "2":
                        Console.Write("\nEnter new student name: ");
                        string name = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(name))
                        {
                            dataService.AddStudent(name);
                            Console.WriteLine($"Student '{name}' added successfully!");
                        }
                        break;

                    case "3":
                        Console.Write("\nEnter current name: ");
                        string oldN = Console.ReadLine() ?? "";
                        Console.Write("Enter new name: ");
                        string newN = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(oldN) && !string.IsNullOrEmpty(newN))
                        {
                            dataService.UpdateStudent(oldN, newN);
                            Console.WriteLine($"Student '{oldN}' updated to '{newN}' successfully!");
                        }
                        break;

                    case "4":
                        Console.Write("\nEnter name to delete: ");
                        string delName = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(delName))
                        {
                            dataService.DeleteStudent(delName);
                            Console.WriteLine($"Student '{delName}' deleted successfully!");
                        }
                        break;

                    case "5":
                        Console.Write($"\nEnter Day to Record Attendance (1 to {totalDays}): ");
                        if (int.TryParse(Console.ReadLine(), out int recDay) && recDay >= 1 && recDay <= totalDays)
                        {
                            Console.WriteLine($"\n--- Day {recDay} ---");
                            appService.RecordDayAttendance(recDay - 1);
                            Console.WriteLine($"Attendance for Day {recDay} recorded successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid day selected.");
                        }
                        break;

                    case "6": 
                        Console.Write("\nEnter Student Name: ");
                        string targetName = Console.ReadLine() ?? "";
                        Console.Write($"Enter Day (1 to {totalDays}): ");
                        if (int.TryParse(Console.ReadLine(), out int targetDay) && targetDay >= 1 && targetDay <= totalDays)
                        {
                            string input = "";
                            while (input != "P" && input != "A")
                            {
                                Console.Write("Enter New Status (P for Present / A for Absent): ");
                                input = (Console.ReadLine() ?? "").ToUpper();
                            }
                            int newStatus = (input == "P") ? 1 : 0;
                           
                            dataService.RecordAttendance(targetName, targetDay - 1, newStatus);
                            Console.WriteLine("Record updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid day selected.");
                        }
                        break;

                    case "7":
                        appService.PrintOverallSummary();
                        break;

                    case "8":
                        running = false;
                        break;
                }
            }
        }
    }
}