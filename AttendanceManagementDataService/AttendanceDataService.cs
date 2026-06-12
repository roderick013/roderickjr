using System.Collections.Generic;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class AttendanceDataService
    {
        private IAttendanceDataService _dataService;

        public AttendanceDataService(IAttendanceDataService dataService)
        {
            _dataService = dataService;
        }

        public List<Student> GetStudents(int totalDays)
        {
            return _dataService.GetStudents(totalDays);
        }

        public void AddStudent(string name)
        {
            _dataService.AddStudent(name);
        }

        public void UpdateStudent(string oldName, string newName)
        {
            _dataService.UpdateStudent(oldName, newName);
        }

        public void DeleteStudent(string name)
        {
            _dataService.DeleteStudent(name);
        }

        public void RecordAttendance(string studentName, int day, int status)
        {
            _dataService.RecordAttendance(studentName, day, status);
        }
    }
}
