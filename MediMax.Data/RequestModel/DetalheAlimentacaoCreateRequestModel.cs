﻿using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class DetalheAlimentacaoCreateRequestModel
    {
        public string alimento { get; set; }
        public int quantidade { get; set; }
        public string unidade_medida { get; set; }
    }
}
