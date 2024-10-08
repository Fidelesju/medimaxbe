﻿using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITimeDosageRepository
    {
        int Create(TimeDosage horarioDosagem);
        void Update(TimeDosage horarioDosagem);
        Task<bool> Update(string horario_dosagem, int Treatment_id);
    }
}
