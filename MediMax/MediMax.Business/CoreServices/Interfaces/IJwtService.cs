namespace MediMax.Business.CoreServices.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(string nameIdentifier, string role);
    }
}