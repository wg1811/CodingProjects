namespace PetHospitalApi.Models
{
    public class MedicalHistoryEntry
    {
        public DateTime Date { get; set; }
        public string? VisitReason { get; set; }
        public string? Treatment { get; set; }
        public string? Prescription { get; set; }
    }
}
