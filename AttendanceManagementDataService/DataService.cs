using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class AttendanceDataService
    {
        
        private readonly string _connectionString = "Server=localhost;Database=AttendanceDB;Uid=root;Pwd=;";

        public List<Student> GetStudents(int totalDays)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    
                    string studentQuery = "SELECT Name FROM Students";
                    using (var cmd = new MySqlCommand(studentQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student(reader["Name"]?.ToString() ?? "Unknown", totalDays));
                        }
                    }

                    
                    foreach (var student in students)
                    {
                        string attendanceQuery = "SELECT DayIndex, Status FROM AttendanceRecord WHERE StudentName = @name";
                        using (var cmd = new MySqlCommand(attendanceQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", student.Name);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int day = Convert.ToInt32(reader["DayIndex"]);
                                    int status = Convert.ToInt32(reader["Status"]);

                                   
                                    if (day >= 0 && day < totalDays)
                                    {
                                        student.Attendance[day] = status;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            return students;
        }
        
        public void AddStudent(string name)
        {
            ExecuteNonQuery("INSERT IGNORE INTO Students (Name) VALUES (@name)",
                new MySqlParameter("@name", name));
        }

     
        public void UpdateStudent(string oldName, string newName)
        {
            
            ExecuteNonQuery("UPDATE Students SET Name = @newName WHERE Name = @oldName",
                new MySqlParameter("@oldName", oldName),
                new MySqlParameter("@newName", newName));
        }


        public void DeleteStudent(string name)
        {
         
            ExecuteNonQuery("DELETE FROM AttendanceRecord WHERE StudentName = @name",
                new MySqlParameter("@name", name));
            ExecuteNonQuery("DELETE FROM Students WHERE Name = @name",
                new MySqlParameter("@name", name));
        }

        public void RecordAttendance(string studentName, int day, int status)
        {
            string query = @"
        INSERT INTO attendancerecord (StudentName, DayIndex, Status) 
        VALUES (@name, @day, @status)
        ON DUPLICATE KEY UPDATE Status = @status";

          
            ExecuteNonQuery(query,
                new MySqlParameter("@name", studentName),
                new MySqlParameter("@day", day),
                new MySqlParameter("@status", status));
        }

        private void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Database Error: {ex.Message}"); }
        }
    }
}