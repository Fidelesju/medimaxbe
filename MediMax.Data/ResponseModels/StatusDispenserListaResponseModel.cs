namespace MediMax.Data.ResponseModels
{
    public class StatusDispenserListaResponseModel
    {
        public int TratamentoId { get; set; }
        public int MedicamentoId { get; set; }
        public int QuantidadeTotalMedicamentoCaixa { get; set; }
        public int IntervaloTratamentoHoras { get; set; }
        public int QuantidadeDiasFaltantesFimTratamento { get; set; }
        public int QuantidadeMedicamentoPorDosagem { get; set; }
        public List<string> DosageTime { get; set; }
    }
}
