namespace MediMax.Data.ResponseModels
{
    public class StatusDispenserResponseModel
    {
        public int Id { get; set; }
        public int TratamentoId { get; set; }
        public int MedicamentoId { get; set; }
        public int QuantidadeTotalMedicamentoCaixa { get; set; }
        public int QuantidadeTotalCaixaTratamento { get; set; }
        public int IntervaloTratamentoHoras { get; set; }
        public int IntervaloTratamentoDias { get; set; }
        public int QuantidadeMedicamentoPorDosagem { get; set; }
        public int FrenqueciaDosagemDiaria { get; set; }
        public int QuantidadeTotalMedicamentoDosagemDia { get; set; }
        public int QuantidadeTotalMedicamentosTratamento { get; set; }
        public int QuantidadeMedicamentoSemanal { get; set; }
        public int QuantidadeAtualMedicamentoCaixaTratamento { get; set; }
        public int QuantidadeMedicamentoFaltanteParaFimTratamento { get; set; }
        public int QuantidadeDiasFaltanteParaFimTratamento { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacaoSemanal { get; set; }
        public string DataInicioTratamento { get; set; }
        public string DataFinalPrevistoTratamento { get; set; }
        public string DataFinalMarcadoTratamento { get; set; }
        public int StatusTratamento { get; set; }

    }
}
