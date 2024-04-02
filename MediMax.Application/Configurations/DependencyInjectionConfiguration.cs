using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.CoreServices;
using MediMax.Business.Mappers;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.Repositories;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Dao;

namespace MediMax.Application.Configurations
{
    public static class DependencyInjectionConfiguration 
    {
        public static void RegisterService(this IServiceCollection services) 
        {
            ConfigureDbDependencyInjection(services);
            ConfigureMapperDependecyInjection(services);
            ConfigureRepositoriesDependecyInjection(services);
            ConfigureServiceDependecyInjection(services);
        }

        private static void ConfigureServiceDependecyInjection(IServiceCollection services) 
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAlimentacaoService, AlimentacaoService>();
            services.AddScoped<IMedicamentoService, MedicamentoService>();
            services.AddScoped<ITratamentoService, TratamentoService>();
            services.AddScoped<IGerenciamentoTratamentoService, GerenciamentoTratamentoService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IFileManagementService, FileManagementService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
        }

        private static void ConfigureDbDependencyInjection(IServiceCollection services) 
        {
            services.AddScoped<IAuthDb, AuthDb>();
            services.AddScoped<IAdminAuthDb, AdminAuthDb>();
            services.AddScoped<IOwnerAuthDb, OwnerAuthDb>();
            services.AddScoped<IOwnerDb, OwnerDb>();
            services.AddScoped<IMedicineDb, MedicineDb>();
            services.AddScoped<ITratamentoDb, TreatmentDb>();
            services.AddScoped<IUsuarioDb, UserDb>();
            services.AddScoped<IAlimentacaoDb, AlimentacaoDb>();

        }

        private static void ConfigureMapperDependecyInjection(IServiceCollection services) 
        {
            services.AddScoped<IUsuarioCreateMapper, UserCreateMapper>();
            services.AddScoped<IMedicamentoCreateMapper, MedicamentoCreateMapper>();
            services.AddScoped<ITratamentoCreateMapper, TratamentoCreateMapper>();
            services.AddScoped<IGerenciamentoTratamentoCreateMapper, GerenciamentoTratamentoCreateMapper>();
            services.AddScoped<IOwnerCreateMapper, OwnerCreateMapper>();
            services.AddScoped<IOwnerUpdateMapper, OwnerUpdateMapper>();
            services.AddScoped<IAlimentacaoCreateMapper, AlimentacaoCreateMapper>();
        }

        private static void ConfigureRepositoriesDependecyInjection(IServiceCollection services)
        {
            services.AddScoped<IMedicamentosRepository, MedicamentoRepository>();
            services.AddScoped<ITratamentoRepository, TratamentoRepository>();
            services.AddScoped<IGerenciamentoTratamentoRepository, GerenciamentoTratamentoRepository>();
            services.AddScoped<IUsuarioRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAlimentacaoRepository, AlimentacaoRepository>();
        }
    }
}
