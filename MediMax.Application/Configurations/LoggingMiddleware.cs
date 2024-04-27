using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;

namespace MediMax.Application.Configurations
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information("Requisição: {Method} {Path}", context.Request.Method, context.Request.Path);

            try
            {
                // Chama o próximo middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Loga a exceção
                Log.Information(ex, "Ocorreu uma exceção ao processar a requisição");
                throw;
            }

            // Loga a resposta
            Log.Information("Resposta: {StatusCode}", context.Response.StatusCode);
        }
    }
}
