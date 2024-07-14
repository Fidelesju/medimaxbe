namespace MediMax.Data.ResponseModels
{
    public class DispenserStatusResponseModel
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int MedicamentoId { get; set; }
        public int QuantidadeTotalMedicamentoCaixa { get; set; }
        public int QuantidadeTotalCaixaTreatment { get; set; }
        public int IntervaloTreatmentHoras { get; set; }
        public int IntervaloTreatmentDias { get; set; }
        public int QuantidadeMedicamentoPorDosagem { get; set; }
        public int FrenqueciaDosagemDiaria { get; set; }
        public int QuantidadeTotalMedicamentoDosagemDia { get; set; }
        public int QuantidadeTotalMedicamentosTreatment { get; set; }
        public int QuantidadeMedicamentoSemanal { get; set; }
        public int QuantidadeAtualMedicamentoCaixaTreatment { get; set; }
        public int QuantidadeMedicamentoFaltanteParaFimTreatment { get; set; }
        public int QuantidadeDiasFaltanteParaFimTreatment { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacaoSemanal { get; set; }
        public string DataInicioTreatment { get; set; }
        public string DataFinalPrevistoTreatment { get; set; }
        public string DataFinalMarcadoTreatment { get; set; }
        public int StatusTreatment { get; set; }

    }
}
