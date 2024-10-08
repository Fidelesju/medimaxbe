﻿using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IRelatoriosService
    {
        Task<byte[]> PdfGenerator ( ReportRequestModel request );
    }
}
