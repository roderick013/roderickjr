using System;

namespace AttendanceManagementModels
{
    public class Student
    {
        public string Name { get; set; }
        public int[] Attendance { get; set; } 

        public Student(string name, int days)
        {
            Name = name;
            Attendance = new int[days];
        }

        public int TotalPresent()
        {
            int total = 0;
            foreach (var status in Attendance)
                total += status;
            return total;
        }
    }
}