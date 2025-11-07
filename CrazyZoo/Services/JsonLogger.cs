using CrazyZoo.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace CrazyZoo.Services
{
    public class JsonLogger : ILogger
    {
        private readonly string _filePath = "log.json";

        public void Log(string message)
        {
            List<object> logs = new List<object>();

            // if there's a file, read existing logs
            if (File.Exists(_filePath))
            {
                string existing = File.ReadAllText(_filePath);
                if (!string.IsNullOrWhiteSpace(existing))
                    logs = JsonConvert.DeserializeObject<List<object>>(existing) ?? new List<object>();
            }

            // add new log entry
            logs.Add(new { Date = DateTime.Now, Message = message });

            // serialize back to file
            string json = JsonConvert.SerializeObject(logs, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
