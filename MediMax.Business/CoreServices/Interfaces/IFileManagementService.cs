namespace MediMax.Business.CoreServices.Interfaces
{
    public interface IFileManagementService
    {
        bool DeleteFile(string filePath);
        bool DeleteFolder(string path);

        string StoreAudioStreamContent(Stream stream, string fileName);

        bool StoreStreamContent(Stream stream, string fileName);

        public string LogFilePath();
        public string GetBaseUrl();
    }
}