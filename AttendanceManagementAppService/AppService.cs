using System;
using System.Collections.Generic;
using AttendanceManagementModels;
using AttendanceManagementDataService;

namespace AttendanceManagementAppService
{
    public class AttendanceAppService
    {
        private AttendanceDataService dataService;

        public AttendanceAppService(AttendanceDataService service)
        {
            dataService = service;
        }

        public void RecordDayAttendance(int day)
        {
            var students = dataService.GetStudents();
            foreach (var student in students)
            {
                Console.Write($"Is {student.Name} present? (P/A): ");
                string input = Console.ReadLine().ToUpper();
                int status = input == "P" ? 1 : 0;
                int index = students.IndexOf(student);
                dataService.RecordAttendance(index, day, status);
            }
        }

        public void PrintDailySummary(int day)
        {
            var students = dataService.GetStudents();
            int present = 0, absent = 0;

            foreach (var student in students)
            {
                if (student.Attendance[day] == 1) present++;
                else absent++;
            }

            Console.WriteLine($"Day {day + 1} Present: {present}");
            Console.WriteLine($"Day {day + 1} Absent: {absent}");
        }

        public void PrintOverallSummary()
        {
            var students = dataService.GetStudents();
            Console.WriteLine("\n=== Each Student Total Attendance ===");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} was present for {student.TotalPresent()} out of {student.Attendance.Length} days.");
            }
        }
    }
}