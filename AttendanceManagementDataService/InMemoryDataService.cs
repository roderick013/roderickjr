using System.Collections.Generic;
using System.Linq;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class InMemoryDataService : IAttendanceDataService
    {
        private readonly List<Student> _students = new List<Student>();

        public List<Student> GetStudents(int totalDays)
        {
            foreach (var student in _students)
            {
                if (student.Attendance.Length < totalDays)
                {
                    int[] newAtt = new int[totalDays];
                    System.Array.Copy(student.Attendance, newAtt, student.Attendance.Length);
                    student.Attendance = newAtt;
                }
            }
            return _students;
        }

        public void AddStudent(string name)
        {
            if (!_students.Any(s => s.Name == name))
            {
                _students.Add(new Student(name, 3));
            }
        }

        public void UpdateStudent(string oldName, string newName)
        {
            var student = _students.FirstOrDefault(s => s.Name == oldName);
            if (student != null)
            {
                student.Name = newName;
            }
        }

        public void DeleteStudent(string name)
        {
            var student = _students.FirstOrDefault(s => s.Name == name);
            if (student != null)
            {
                _students.Remove(student);
            }
        }

        public void RecordAttendance(string studentName, int day, int status)
        {
            var student = _students.FirstOrDefault(s => s.Name == studentName);
            if (student != null)
            {
                if (day < student.Attendance.Length)
                {
                    student.Attendance[day] = status;
                }
            }
        }
    }
}
