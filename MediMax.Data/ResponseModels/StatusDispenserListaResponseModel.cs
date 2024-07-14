namespace MediMax.Data.ResponseModels
{
    public class DispenserStatusListaResponseModel
    {
        public int TreatmentId { get; set; }
        public int MedicamentoId { get; set; }
        public int QuantidadeTotalMedicamentoCaixa { get; set; }
        public int IntervaloTreatmentHoras { get; set; }
        public int QuantidadeDiasFaltantesFimTreatment { get; set; }
        public int QuantidadeMedicamentoPorDosagem { get; set; }
        public List<string> DosageTime { get; set; }
    }
}
