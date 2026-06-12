    using System;
    using System.Collections.Generic;
    using AttendanceManagementModels;
    using AttendanceManagementDataService;

    namespace AttendanceManagementAppService
    {
        public class AttendanceAppService
        {
            private AttendanceDataService _dataService = new AttendanceDataService(new MySqlDataService());
            private int _totalDays;

            public AttendanceAppService(int totalDays)
            {
                _totalDays = totalDays;
            }

            public void RecordDayAttendance(int day)
            {
            
                var students = _dataService.GetStudents(_totalDays);

                foreach (var student in students)
                {
                    string input = "";
                    while (input != "P" && input != "A")
                    {
                        Console.Write($"Is {student.Name} present? (P/A): ");
                        input = (Console.ReadLine() ?? "").ToUpper();
                    }
                    int status = (input == "P") ? 1 : 0;

                
                    _dataService.RecordAttendance(student.Name, day, status);

                   
                    student.Attendance[day] = status;
                }
            }

            public void PrintDailySummary(int day)
            {
                var students = _dataService.GetStudents(_totalDays);
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
            var students = _dataService.GetStudents(_totalDays);

            Console.WriteLine("\n=== Detailed Attendance Summary Per Student ===");
            foreach (var student in students)
            {
                Console.WriteLine($"\nStudent: {student.Name}");
                Console.WriteLine("-------------------------");

                for (int day = 0; day < _totalDays; day++)
                {
                   
                    string statusText = (student.Attendance[day] == 1) ? "Present" : "Absent";
                    Console.WriteLine($"Day {day + 1}: {statusText}");
                }

                Console.WriteLine($"Total: {student.TotalPresent()} out of {_totalDays} days present.");
            }
        }
    }
    }