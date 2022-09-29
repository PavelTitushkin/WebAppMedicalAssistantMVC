namespace WebAppMedicalAssistant_Core.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? DescriptionOfDestination { get; set; }


        public List<MedicalExaminationDto>? MedicalExaminationsDto { get; set; }
        public List<PhysicalTherapyDto>? PhysicalTherapysDto { get; set; }
        public List<PrescribedMedicationDto>? PrescribedMedicationsDto { get; set; }
        public DoctorVisitDto? DoctorVisitDto { get; set; }
    }
}
