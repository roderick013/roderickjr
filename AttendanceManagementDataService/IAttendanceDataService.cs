using System.Collections.Generic;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public interface IAttendanceDataService
    {
        List<Student> GetStudents(int totalDays);
        void AddStudent(string name);
        void UpdateStudent(string oldName, string newName);
        void DeleteStudent(string name);
        void RecordAttendance(string studentName, int day, int status);
    }
}
