using AutoMapper;
using MediMax.Data.Dao;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Business.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbDataReader, LoginResponseModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["UserId"]))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["Name"]))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src["Email"]))
               .ForMember(dest => dest.TypeUserId, opt => opt.MapFrom(src => src["TypeUserId"]))
               .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src["OwnerId"]));

            CreateMap<DbDataReader, UserResponseModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["UserId"]))
               .ForMember(dest => dest.Name_User, opt => opt.MapFrom(src => src["Name"]))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src["Email"]))
               .ForMember(dest => dest.Type_User_Id, opt => opt.MapFrom(src => src["TypeUser"]))
               .ForMember(dest => dest.Owner_Id, opt => opt.MapFrom(src => src["OwnerId"]))
               .ForMember(dest => dest.Is_Active, opt => opt.MapFrom(src => src["IsActive"]));
            
            CreateMap<DbDataReader, TreatmentManagementResponseModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["Id"]))
               .ForMember(dest => dest.Treatment_Id, opt => opt.MapFrom(src => src["TreatmentId"]))
               .ForMember(dest => dest.Treatment_User_Id, opt => opt.MapFrom(src => src["TreatmentUserId"]))
               .ForMember(dest => dest.Correct_Time_Treatment, opt => opt.MapFrom(src => src["CorrectTimeTreatment"]))
               .ForMember(dest => dest.Medication_Intake_Time, opt => opt.MapFrom(src => src["MedicationIntakeTime"]))
               .ForMember(dest => dest.Was_Taken, opt => opt.MapFrom(src => src["WasTaken"]))
               .ForMember(dest => dest.Medication_Id, opt => opt.MapFrom(src => src["MedicationId"]))
               .ForMember(dest => dest.Medication_Intake_Date, opt => opt.MapFrom(src => src["MedicationIntakeDate"]));

            CreateMap<DbDataReader, MedicationResponseModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src["Id"]))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src["UserId"]))
                .ForMember(dest => dest.medicine_name, opt => opt.MapFrom(src => src["NameMedication"]))
                .ForMember(dest => dest.expiration_date, opt => opt.MapFrom(src => src["ExpirationDate"]))
                .ForMember(dest => dest.dosage, opt => opt.MapFrom(src => src["Dosage"]))
                .ForMember(dest => dest.package_quantity, opt => opt.MapFrom(src => src["PackageQuantity"]))
                .ForMember(dest => dest.is_active, opt => opt.MapFrom(src => src["IsActive"])); ;


            CreateMap<DbDataReader, TimeDosageResponseModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["Id"]))
                .ForMember(dest => dest.Treatment_Id, opt => opt.MapFrom(src => src["TreatmentId"]))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src["Time"]));

            CreateMap<DbDataReader, TreatmentResponseModel>()
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["MedicineId"]))
                          .ForMember(dest => dest.Name_Medication, opt => opt.MapFrom(src => src["NameMedication"]))
                          .ForMember(dest => dest.Medication_Quantity, opt => opt.MapFrom(src => src["MedicineQuantity"]))
                          .ForMember(dest => dest.Start_Time, opt => opt.MapFrom(src => src["StartTime"]))
                          .ForMember(dest => dest.Treatment_Interval_Hours, opt => opt.MapFrom(src => src["TreatmentIntervalHours"]))
                          .ForMember(dest => dest.Treatment_Interval_Days, opt => opt.MapFrom(src => src["TreatmentDurationDays"]))
                          .ForMember(dest => dest.Dietary_Recommendations, opt => opt.MapFrom(src => src["DietaryRecommendations"]))
                          .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src["Observation"]))
                          .ForMember(dest => dest.Is_Active, opt => opt.MapFrom(src => src["IsActive"]))
                          .ForMember(dest => dest.Continuous_Use, opt => opt.MapFrom(src => src["ContinuousUse"]))
                          .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src["UserId"]));

            CreateMap<UserCreateRequestModel, User>();
            CreateMap<TimeDosageCreateRequestModel, TimeDosage>();
            CreateMap<TreatmentCreateRequestModel, Treatment>();
            CreateMap<MedicationCreateRequestModel, Medication>();
            CreateMap<TreatmentManagementCreateRequestModel, TreatmentManagement>();
            CreateMap<UserUpdateRequestModel, UserResponseModel>();
            CreateMap<MedicationUpdateRequestModel, MedicationResponseModel>();
            CreateMap<TreatmentUpdateRequestModel, TreatmentResponseModel>();
        }
    }
}
