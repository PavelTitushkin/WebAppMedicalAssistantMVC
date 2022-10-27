namespace WebAppMedicalAssistant_Core.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? DescriptionOfDestination { get; set; }

        public string? ReturnUrl { get; set; }
        public int? DoctorVisitId { get; set; }
        public int? TransferredDiseaseId { get; set; }
        public List<MedicalExaminationDto>? MedicalExaminationsDto { get; set; }
        public List<PhysicalTherapyDto>? PhysicalTherapysDto { get; set; }
        public List<PrescribedMedicationDto>? PrescribedMedicationsDto { get; set; }
        public List<AnalysisDto>? AnalysisDto { get; set; }
    }
}
