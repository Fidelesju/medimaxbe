namespace MediMax.Data.Dao
{
    public class AcessDb
    {
        public readonly IConfiguration Configuration;
        public readonly IWebHostEnvironment HostingEnviroment;
        public readonly string StringConnection = string.Empty;

        public AcessDb(IConfiguration configuration,
            IWebHostEnvironment hostingEnviroment)
        {
            Configuration = configuration;
            HostingEnviroment = hostingEnviroment;
            StringConnection = "server=localhost;port=3306;database=medimaxdb;user=fidelesju;password=gCR7!5dDpXtc&9o; Persist Security Info= False; Connect Timeout=300";
        }
    }
}