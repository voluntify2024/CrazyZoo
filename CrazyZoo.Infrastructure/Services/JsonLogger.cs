using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CrazyZoo.Infrastructure.Services
{
    public class JsonLogger : ILogger
    {
        private readonly string _filePath = "log.json";

        public void Log(string message)
        {
            List<object> logs = new List<object>();

            // if file exists, read and deserialize existing logs
            if (File.Exists(_filePath))
            {
                string existing = File.ReadAllText(_filePath);
                if (!string.IsNullOrWhiteSpace(existing))
                {
                    try
                    {
                        logs = JsonSerializer.Deserialize<List<object>>(existing)
                               ?? new List<object>();
                    }
                    catch
                    {
                        // if file is corrupted, start fresh
                        logs = new List<object>();
                    }
                }
            }

            logs.Add(new { Date = DateTime.Now, Message = message });

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(logs, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
