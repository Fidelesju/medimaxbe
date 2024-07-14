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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INutritionService, NutritionService>();
            services.AddScoped<IMedicationService, MedicationService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<ITreatmentManagementService, TreatmentManagementService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IFileManagementService, FileManagementService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IDispenserStatusService, DispenserStatusService>();
            services.AddScoped<IRelatoriosService, RelatoriosService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void ConfigureDbDependencyInjection(IServiceCollection services) 
        {
            services.AddScoped<IAuthDb, AuthDb>();
            services.AddScoped<IAdminAuthDb, AdminAuthDb>();
            services.AddScoped<IOwnerAuthDb, OwnerAuthDb>();
            services.AddScoped<IOwnerDb, OwnerDb>();
            services.AddScoped<IMedicationDb, MedicationDb>();
            services.AddScoped<ITreatmentDb, TreatmentDb>();
            services.AddScoped<IUserDb, UserDb>();
            services.AddScoped<INutritionDb, NutritionDb>();
            services.AddScoped<IHorariosDosagemDb, HorariosDosagemDb>();
            services.AddScoped<IHistoricoDb, HistoricoDb>();
            services.AddScoped<IDispenserStatusDb, DispenserStatusDb>();
        }

        private static void ConfigureMapperDependecyInjection(IServiceCollection services) 
        {
            services.AddScoped<IUserCreateMapper, UserCreateMapper>();
            services.AddScoped<IUserUpdateMapper, UserUpdateMapper>();
            services.AddScoped<IMedicationCreateMapper, MedicamentoCreateMapper>();
            services.AddScoped<IMedicationUpdateMapper, MedicationUpdateMapper>();
            services.AddScoped<ITreatmentCreateMapper, TreatmentCreateMapper>();
            services.AddScoped<ITreatmentUpdateMapper, TreatmentUpdateMapper>();
            services.AddScoped<IDetalheAlimentacaoCreateMapper, DetalheAlimentacaoCreateMapper>();
            services.AddScoped<ITreatmentManagementCreateMapper, TreatmentManagementCreateMapper>();
            services.AddScoped<IOwnerCreateMapper, OwnerCreateMapper>();
            services.AddScoped<IOwnerUpdateMapper, OwnerUpdateMapper>();
            services.AddScoped<INutritionCreateMapper, NutritionCreateMapper>();
            services.AddScoped<IHorarioDosagemCreateMapper, HorarioDosagemCreateMapper>();
            services.AddScoped<INotificationCreateMapper, NotificationCreateMapper>();
            services.AddScoped<IDispenserStatusCreateMapper, DispenserStatusCreateMapper>();
        }

        private static void ConfigureRepositoriesDependecyInjection(IServiceCollection services)
        {
            services.AddScoped<IMedicationRepository, MedicamentoRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<ITreatmentManagementRepository, TreatmentManagementRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<INutritionRepository, NutritionRepository>();
            services.AddScoped<IDetalheAlimentacaoRepository, DetalheAlimentacaoRepository>();
            services.AddScoped<IHorarioDosagemRepository, HorarioDosagemRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IDispenserStatusRepository, DispenserStatusRepository>();
        }
    }
}
