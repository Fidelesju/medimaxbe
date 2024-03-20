using MediMax.Business.CoreServices.Interfaces;

namespace MediMax.Business.CoreServices
{
    public class FileManagementService : IFileManagementService
    {
        private const string UploadsFolder = "wwwroot/uploads";
        private const string AudiosFolder = "audios";
        private readonly IConfiguration _configuration;
        public string UploadsAbsoluteFolder;

        public FileManagementService(IConfiguration configuration)
        {
            _configuration = configuration;
            UploadsAbsoluteFolder = configuration.GetValue<string>("HostSettings:BaseUrl");
        }

        public string GetBaseUrl()
        {
            return _configuration.GetValue<string>("HostSettings:BaseUrl");
        }


        public string LogFilePath()
        {
            string directory;
            string basePath;
            string logsFolder;

            directory = Directory.GetCurrentDirectory();
            basePath = Path.Combine(directory, "wwwroot", "logs");
            logsFolder = Path.Combine(basePath, $"log-{DateTime.Now:yyyy-M-d_dddd}.log");
            return logsFolder;
        }
        public bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(UploadsFolder + "/" + filePath);
                // _loggerService.LogInfo($"The file {filePath} was deleted.");
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                // _loggerService.LogInfo($"Failure on deletion of file {filePath}: {exception.Message}");
                // _loggerService.LogErrorServicesBackground(exception);
                return false;
            }
        }

        public bool DeleteFolder(string path)
        {
            try
            {
                Directory.Delete(UploadsFolder + "/" + path, true);
                // _loggerService.LogInfo($"The file {filePath} was deleted.");
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                // _loggerService.LogInfo($"Failure on deletion of file {filePath}: {exception.Message}");
                // _loggerService.LogErrorServicesBackground(exception);
                return false;
            }
        }

        public string StoreAudioStreamContent(Stream stream, string fileName)
        {
            string outputFileName;
            string folder;
            string outFolder;
            string outFileName;

            folder = $"{UploadsFolder}/audios";

            outFolder = $"{UploadsAbsoluteFolder}/audios";
            outFileName = $"{Guid.NewGuid()}_{fileName}.mp3";
            outputFileName = $"{folder}/{outFileName}";

            if (!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return "";
                }
            }

            if (!StoreStreamContent(stream, outputFileName))
            {
                return "";
            }

            outputFileName = $"{outFolder}/{outFileName}";
            return outputFileName;
        }

        public bool StoreStreamContent(Stream stream, string fileName)
        {
            FileStream output;
            try
            {
                output = File.Open(fileName, FileMode.Create);
                stream.CopyTo(output);
                output.Close();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}