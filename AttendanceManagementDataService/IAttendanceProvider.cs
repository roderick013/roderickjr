using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class MySqlAttendanceDataService
    {
        private string _connectionString;

        public MySqlAttendanceDataService(string server, string database, string user, string password)
        {
            _connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
        }

        public List<Student> GetStudents(int totalDays)
        {
            List<Student> students = new List<Student>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT Id, Name FROM Students";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student(reader["Name"].ToString(), totalDays);
                        
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public void RecordAttendance(string studentName, int day, int status)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
              
                string query = @"
                    INSERT INTO Attendance (StudentId, DayIndex, Status) 
                    VALUES ((SELECT Id FROM Students WHERE Name = @name), @day, @status)
                    ON DUPLICATE KEY UPDATE Status = @status";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", studentName);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}