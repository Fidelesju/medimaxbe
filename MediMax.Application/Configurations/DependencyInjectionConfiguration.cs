using MediMax.Business.CoreServices;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Mappers;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.RealTimeServices;
using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Business.Services;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories;
using MediMax.Data.Repositories.Interfaces;

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
            services.AddScoped<IStatusDispenserService, StatusDispenserService>();
            services.AddScoped<IRelatoriosService, RelatoriosService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void ConfigureDbDependencyInjection(IServiceCollection services) 
        {
            services.AddScoped<IAuthDb, AuthDb>();
            services.AddScoped<IAdminAuthDb, AdminAuthDb>();
            services.AddScoped<IOwnerAuthDb, OwnerAuthDb>();
            services.AddScoped<IOwnerDb, OwnerDb>();
            services.AddScoped<IMedicamentoDb, MedicamentoDb>();
            services.AddScoped<ITratamentoDb, TratamentoDb>();
            services.AddScoped<IUsuarioDb, UserDb>();
            services.AddScoped<IAlimentacaoDb, AlimentacaoDb>();
            services.AddScoped<IHorariosDosagemDb, HorariosDosagemDb>();
            services.AddScoped<IHistoricoDb, HistoricoDb>();
            services.AddScoped<IStatusDispenserDb, StatusDispenserDb>();
        }

        private static void ConfigureMapperDependecyInjection(IServiceCollection services) 
        {
            services.AddScoped<IUsuarioCreateMapper, UserCreateMapper>();
            services.AddScoped<IUsuarioUpdateMapper, UserUpdateMapper>();
            services.AddScoped<IMedicamentoCreateMapper, MedicamentoCreateMapper>();
            services.AddScoped<ITratamentoCreateMapper, TratamentoCreateMapper>();
            services.AddScoped<IDetalheAlimentacaoCreateMapper, DetalheAlimentacaoCreateMapper>();
            services.AddScoped<IGerenciamentoTratamentoCreateMapper, GerenciamentoTratamentoCreateMapper>();
            services.AddScoped<IOwnerCreateMapper, OwnerCreateMapper>();
            services.AddScoped<IOwnerUpdateMapper, OwnerUpdateMapper>();
            services.AddScoped<IAlimentacaoCreateMapper, AlimentacaoCreateMapper>();
            services.AddScoped<IHorarioDosagemCreateMapper, HorarioDosagemCreateMapper>();
            services.AddScoped<INotificacaoCreateMapper, NotificacaoCreateMapper>();
            services.AddScoped<IStatusDispenserCreateMapper, StatusDispenserCreateMapper>();
        }

        private static void ConfigureRepositoriesDependecyInjection(IServiceCollection services)
        {
            services.AddScoped<IMedicamentosRepository, MedicamentoRepository>();
            services.AddScoped<ITratamentoRepository, TratamentoRepository>();
            services.AddScoped<IGerenciamentoTratamentoRepository, GerenciamentoTratamentoRepository>();
            services.AddScoped<IUsuarioRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAlimentacaoRepository, AlimentacaoRepository>();
            services.AddScoped<IDetalheAlimentacaoRepository, DetalheAlimentacaoRepository>();
            services.AddScoped<IHorarioDosagemRepository, HorarioDosagemRepository>();
            services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
            services.AddScoped<IStatusDispenserRepository, StatusDispenserRepository>();
        }
    }
}
