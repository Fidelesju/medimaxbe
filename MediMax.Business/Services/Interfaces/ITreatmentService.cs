﻿using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId(int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId );
        Task<bool> DeactivateTreatment ( int medicineId, int treatmentId );
        Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId );
        Task<List<TreatmentResponseModel>> GetListTreatmentById ( IdTreatmentListRequestModel request );
        Task<List<TimeDosageResponseModel>> GetDosageTimeByTreatmentId ( int treatmentId );
        Task<int> CreateTreatment ( TreatmentCreateRequestModel request );
        Task<bool> UpdateMedication ( TreatmentUpdateRequestModel request );
        Task<bool> ReactiveTreatment ( int medicineId, int treatmentId );
        Task<List<TreatmentResponseModel>> GetTreatmentDeactivate ( int userId );
        Task<List<TimeDosageResponseModel>> GetDosageTimeByUserIdAndTime ( int userId, string time );
    }
}
