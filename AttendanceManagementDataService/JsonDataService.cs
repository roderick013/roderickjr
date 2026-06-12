using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class JsonDataService : IAttendanceDataService
    {
        private readonly string _filePath;

        public JsonDataService(string filePath = "AttendanceBackup.json")
        {
            _filePath = filePath;
        }

        public void SaveToJson(List<Student> students)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);
                File.WriteAllText(_filePath, jsonString);
                Console.WriteLine($"\n[JSON] Success! Data synced to: {Path.GetFullPath(_filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[JSON ERROR]: Could not save file. {ex.Message}");
            }
        }

        public List<Student> LoadFromJson()
        {
            if (!File.Exists(_filePath)) return new List<Student>();

            try
            {
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
            }
            catch { return new List<Student>(); }
        }

        // --- IAttendanceDataService Implementation ---

        public List<Student> GetStudents(int totalDays)
        {
            var students = LoadFromJson();
            // Ensure they have the correct number of days
            foreach (var student in students)
            {
                if (student.Attendance.Length < totalDays)
                {
                    int[] newAtt = new int[totalDays];
                    System.Array.Copy(student.Attendance, newAtt, student.Attendance.Length);
                    student.Attendance = newAtt;
                }
            }
            return students;
        }

        public void AddStudent(string name)
        {
            var students = LoadFromJson();
            if (!students.Any(s => s.Name == name))
            {
                students.Add(new Student(name, 3)); // defaults to 3 days for new ones
                SaveToJson(students);
            }
        }

        public void UpdateStudent(string oldName, string newName)
        {
            var students = LoadFromJson();
            var student = students.FirstOrDefault(s => s.Name == oldName);
            if (student != null)
            {
                student.Name = newName;
                SaveToJson(students);
            }
        }

        public void DeleteStudent(string name)
        {
            var students = LoadFromJson();
            var student = students.FirstOrDefault(s => s.Name == name);
            if (student != null)
            {
                students.Remove(student);
                SaveToJson(students);
            }
        }

        public void RecordAttendance(string studentName, int day, int status)
        {
            var students = LoadFromJson();
            var student = students.FirstOrDefault(s => s.Name == studentName);
            if (student != null)
            {
                if (day < student.Attendance.Length)
                {
                    student.Attendance[day] = status;
                }
                SaveToJson(students);
            }
        }
    }
}