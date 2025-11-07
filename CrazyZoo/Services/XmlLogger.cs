using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CrazyZoo.Interfaces;
using System.IO;

namespace CrazyZoo.Services
{
    public class XmlLogger : ILogger
    {
        private readonly string _filePath = "log.xml";

        public void Log(string message)
        {
            XDocument doc;

            // if there's a file
            if (File.Exists(_filePath))
                doc = XDocument.Load(_filePath);
            else
                doc = new XDocument(new XElement("Logs")); //create new

            // add new log entry
            doc.Root.Add(new XElement("Log",
                new XAttribute("Date", DateTime.Now),
                new XElement("Message", message)));

            // save back to file
            doc.Save(_filePath);
        }
    }
}
