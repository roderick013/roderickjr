using System;
using System.Collections.Generic;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class AttendanceDataService
    {
        private List<Student> students;

        public AttendanceDataService(string[] studentNames, int days)
        {
            students = new List<Student>();
            foreach (var name in studentNames)
                students.Add(new Student(name, days));
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        public void RecordAttendance(int studentIndex, int day, int status)
        {
            if (studentIndex >= 0 && studentIndex < students.Count && day >= 0 && day < students[studentIndex].Attendance.Length)
            {
                students[studentIndex].Attendance[day] = status;
            }
        }
    }
}