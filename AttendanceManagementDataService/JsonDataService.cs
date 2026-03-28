using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    public class JsonDataService
    {
        private readonly string _filePath = "AttendanceBackup.json";

        public void SaveToJson(List<Student> students)
        {
            try
            {
                // Options to make the JSON file easy for humans to read
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
    }
}