using System.Diagnostics;
using System.Security.Claims;
using MediMax.Business.CoreServices.Interfaces;

namespace MediMax.Business.CoreServices
{
    public class LoggerService : ILoggerService
    {
        private readonly IFileManagementService _fileManagementService;

        public LoggerService(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        public Task LogError(Exception exception, HttpContext context)
        {
            string directory;
            string basePath;
            string filePath;
            StackTrace stackTrace;
            StackFrame stackFrame;
            int line;
            string query;
            string data;

            directory = Directory.GetCurrentDirectory();
            basePath = Path.Combine(directory, "wwwroot", "logs");

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            filePath = Path.Combine(basePath, $"{DateTime.Now:yyyyMMdd}LogError");
            stackTrace = new StackTrace(exception, true);
            stackFrame = stackTrace.GetFrame(0);
            if (stackFrame == null)
            {
                return Task.CompletedTask;
            }

            line = stackFrame.GetFileLineNumber();
            using StreamWriter streamWriter = File.AppendText(filePath);
            query = string.Join(';', context.Request.Query.Select(a => $" {a.Key}: {a.Value}").ToList());
            streamWriter.WriteLine($"DateTime:  {DateTime.Now}");
            streamWriter.WriteLine($"Path: {context.Request.Path}");
            streamWriter.WriteLine($"Query:{query}");
            streamWriter.WriteLine($"Logged User: {context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
            streamWriter.WriteLine($"Line: {line}");
            if (context.Request.HasFormContentType)
            {
                data = string.Join(';', context.Request.Form.Select(a => $" {a.Key}: {a.Value}").ToList());
                streamWriter.WriteLine($"Data:{data}");
            }

            streamWriter.WriteLine($"Exception Type: {exception.GetType().Name}");
            streamWriter.WriteLine($"Message: {exception.Message}");
            streamWriter.WriteLine($"Inner Exception: {exception.InnerException}");
            streamWriter.WriteLine();

            return Task.CompletedTask;
        }

        public Task LogErrorServicesBackground(Exception exception)
        {
            string filePath;
            filePath = _fileManagementService.LogFilePath();
            using StreamWriter streamWriter = File.AppendText(filePath);
            while (exception != null)
            {
                WriteLog(streamWriter, exception);
                exception = exception.InnerException;
            }

            return Task.CompletedTask;
        }

        public Task LogInfo(string info)
        {
            string filePath;
            filePath = _fileManagementService.LogFilePath();
            using StreamWriter streamWriter = File.AppendText(filePath);
            WriteLog(streamWriter, info);
            return Task.CompletedTask;
        }

        private static void WriteLog(TextWriter streamWriter, string message)
        {
            string time;
            time = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            streamWriter.WriteLine($"\n[{time}] {message}");
        }

        private void WriteLog(TextWriter streamWriter, Exception exception)
        {
            string time;
            // TODO: Store http context information here.
            time = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            streamWriter.WriteLine($"\n[{time}] {exception.Message}");
            streamWriter.WriteLine("[stacktrace]");
            streamWriter.WriteLine($"{StackTraceText(exception)}");
        }

        private static string StackTraceText(Exception exception)
        {
            int i;
            int length;
            string stackTraceText;
            List<string> stackTraceList;

            stackTraceText = "";
            if (exception.StackTrace != null)
            {
                stackTraceList = exception.StackTrace.Split("   at").ToList();
                for (i = 1, length = stackTraceList.Count; i < length; i++)
                {
                    stackTraceText += $"#{i} at{stackTraceList[i]}";
                }
            }

            return stackTraceText;
        }
    }
}