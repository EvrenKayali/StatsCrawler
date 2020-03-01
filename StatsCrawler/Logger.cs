using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StatsCrawler
{

    public class Logger
    {
        string path = AppDomain.CurrentDomain.BaseDirectory+"\\logs\\";
        public TimeSpan LogTime { get; set; }
        public string LogFunction { get; set; }
        public string LogMessage { get; set; }
        public EnLogType LogType { get; set; }

        public void InsertErrorLog(string function, string logMessage, EnLogType logType)
        {
            LogTime = DateTime.Now.TimeOfDay;
            LogFunction = function;
            LogMessage = logMessage;
            LogType = logType;
            string json = JsonConvert.SerializeObject(this);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filePath = GetLogPath();
            TextWriter twriter = new StreamWriter(filePath,true);
            if (!File.Exists(filePath))
                File.Create(filePath);
            twriter.WriteLine(json);
            twriter.Close();
        }


        public enum EnLogType
        {
            Audit=0,
            Error=1
        }

        string GetLogPath()
        {
            return path + DateTime.Now.ToString("ddMMyyyy") + "log.json";
        }
    }
}
